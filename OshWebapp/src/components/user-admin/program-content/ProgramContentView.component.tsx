import { AppShell, Button, Checkbox, Group, Modal, Select, Stack, Tabs, Text, TextInput, Title } from "@mantine/core";
import { Route } from "../../../routes/admin/program/$id.tsx";
import { AppShellAdmin } from "../AppShellAdmin.component.tsx";
import { Bolt, GraduationCap, ShieldCheck } from "lucide-react";
import { getQuery } from "../../../api/crud.ts";
import { auth } from "../../../api/api.ts";
import { useEffect, useState } from "react";
import { OshProgram } from "../../../models/domain/OshProgram.ts";
import { MaterialLearning } from "./MaterialLearning.component.tsx";
import { MaterialTraining } from "./MaterialTraining.component.tsx";
import { useNavigate } from "@tanstack/react-router";
import { Specialty } from "../../../models/domain/Specialty.ts";
import { isNotEmpty, matches, useForm } from "@mantine/form";

const programApi = getQuery<OshProgram>("/api/osh-program", auth);
const specialtyApi = getQuery<Specialty>("/api/specialty", auth);

function ProgramContentView() {
  const { id } = Route.useParams();
  const [program, setProgram] = useState<OshProgram>();

  async function loadProgram() {
    const res = await programApi.getById(parseInt(id));
    if (res) {
      setProgram(res);
    }
  }

  useEffect(() => {
    (async () => {
      await loadProgram();
      await load();
      await search();
    })();
  }, []);

  const navigate = useNavigate();
  const [deleteOpen, setDeleteOpen] = useState(false);
  const [loading, setLoading] = useState(false);
  const [select, setSelect] = useState<{ value: string; label: string }[]>([]);
  const [searchV, setSearchV] = useState("");
  const [checked, setChecked] = useState(false);
  useEffect(() => {
    (async () => await search())();
  }, [searchV]);
  const search = async () => {
    const res = await specialtyApi.search(searchV, 0, 10);
    const content = res.content as Specialty[];
    setSelect(content.map((x) => ({ value: x.id.toString(), label: x.name })));
  };
  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      name: "",
      description: "",
      learningMinutesDuration: "",
      trainingMinutesDuration: "",
      specialityId: "",
      trainingSuccessRate: "",
    },
    validate: {
      name: isNotEmpty("Название не может быть пустым"),
      learningMinutesDuration: matches(/^[1-9]\d*$/, "Целое число больше 0"),
      trainingMinutesDuration: matches(/^[1-9]\d*$/, "Целое число больше 0"),
      specialityId: isNotEmpty("Специальность должна быть заполнена"),
      trainingSuccessRate: matches(
        /^(?:100(?:\.0{1,2})?|\d{1,2}(?:\.\d{1,2})?)$/,
        "Число процентов (от 0.00 до 100.00)",
      ),
    },
  });

  async function load() {
    const value = await programApi.getById(parseInt(id));
    if (value) {
      form.setFieldValue("name", value.name);
      form.setFieldValue("description", value.description);
      form.setFieldValue("learningMinutesDuration", value.learningMinutesDuration.toString());
      form.setFieldValue("trainingMinutesDuration", value.trainingMinutesDuration.toString());
      form.setFieldValue("specialityId", value.specialty?.id.toString());
      form.setFieldValue("trainingSuccessRate", value.trainingSuccessRate.toString());
      form.resetDirty({
        ...value,
        learningMinutesDuration: value.learningMinutesDuration.toString(),
        trainingMinutesDuration: value.trainingMinutesDuration.toString(),
        specialityId: value.specialty?.id.toString(),
        trainingSuccessRate: value.trainingSuccessRate.toString(),
      });
      setSearchV(value.specialty?.name);
    }
  }

  return (
    <AppShellAdmin>
      <AppShell.Main>
        <Tabs defaultValue="info">
          <Stack h="100%" align="flex-start" justify="center" gap="md" px={40} py={5}>
            <Group justify="flex-start" w="100%" align="start">
              <Title order={1}>
                #{id} {program?.name}
              </Title>

              <Tabs.List>
                <Tabs.Tab value="info" leftSection={<Bolt />}>
                  Общие параметры
                </Tabs.Tab>
                <Tabs.Tab value="learning" leftSection={<GraduationCap />}>
                  Обучающие материалы
                </Tabs.Tab>
                <Tabs.Tab value="training" leftSection={<ShieldCheck />}>
                  Вопросы теста
                </Tabs.Tab>
              </Tabs.List>
            </Group>
            <Tabs.Panel value="info" style={{ width: "50%" }}>
              <form
                style={{ width: "100%" }}
                onSubmit={form.onSubmit(
                  async (values) => {
                    const val = {
                      ...values,
                      learningMinutesDuration: parseInt(values.learningMinutesDuration),
                      trainingMinutesDuration: parseInt(values.trainingMinutesDuration),
                      specialityId: parseInt(values.specialityId),
                      autoAssignmentType: "FullManual",
                      trainingSuccessRate: parseFloat(values.trainingSuccessRate),
                    };
                    console.log(val);
                    setLoading(true);
                    const res = await programApi.update(parseInt(id), { ...val });
                    setLoading(false);
                    if (res != "ERROR") {
                      await loadProgram();
                      await load();
                      await search();
                      setChecked(false);
                    }
                  },
                  () => {
                    form.resetTouched();
                  },
                )}>
                <Group align="baseline">
                  <Stack gap="0" w="45%">
                    <TextInput
                      size="sm"
                      label="Название"
                      key={form.key("name")}
                      {...form.getInputProps("name")}
                      readOnly={!checked}
                    />
                    <Text size="sm" c="dimmed">
                      Название программы, используется для поиска
                    </Text>
                  </Stack>
                  <TextInput
                    size="sm"
                    label="Описание"
                    w="52.711%"
                    key={form.key("description")}
                    {...form.getInputProps("description")}
                    readOnly={!checked}
                  />
                </Group>

                <TextInput
                  size="sm"
                  label="Продолжительность обучения"
                  w="100%"
                  mt="md"
                  key={form.key("learningMinutesDuration")}
                  {...form.getInputProps("learningMinutesDuration")}
                  readOnly={!checked}
                />
                <Text size="sm" c="dimmed">
                  Количество минут, требуемое на изучение материала
                </Text>

                <TextInput
                  size="sm"
                  label="Продолжительность тестирования"
                  w="100%"
                  mt="md"
                  key={form.key("trainingMinutesDuration")}
                  {...form.getInputProps("trainingMinutesDuration")}
                  readOnly={!checked}
                />
                <Text size="sm" c="dimmed">
                  Максимальное время на решение теста в минутах
                </Text>

                <Select
                  label="Специальность"
                  w="100%"
                  mt="md"
                  searchable
                  searchValue={searchV}
                  onSearchChange={setSearchV}
                  data={select}
                  key={form.key("specialityId")}
                  {...form.getInputProps("specialityId")}
                  readOnly={!checked}
                />

                <TextInput
                  size="sm"
                  label="Процент успеха теста"
                  w="100%"
                  mt="md"
                  key={form.key("trainingSuccessRate")}
                  {...form.getInputProps("trainingSuccessRate")}
                  readOnly={!checked}
                />
                <Text size="sm" c="dimmed">
                  Соотношение количества правильных вопросов, не правильным
                </Text>

                <Group align="center" mt="md">
                  <Button color="orange" radius="md" type="submit" loading={loading} disabled={!checked}>
                    Сохранить изменения
                  </Button>
                  <Checkbox
                    label="Разрешить изменения"
                    checked={checked}
                    onChange={(event) => setChecked(event.currentTarget.checked)}
                  />
                  <Button color="red" radius="md" onClick={() => setDeleteOpen(true)}>
                    Удалить программу обучения
                  </Button>
                </Group>
              </form>
            </Tabs.Panel>

            <Tabs.Panel value="learning" style={{ width: "90%" }}>
              <MaterialLearning programId={parseInt(id)} />
            </Tabs.Panel>

            <Tabs.Panel value="training" style={{ width: "90%" }}>
              <MaterialTraining programId={parseInt(id)} />
            </Tabs.Panel>
          </Stack>
        </Tabs>
      </AppShell.Main>
      <Modal opened={deleteOpen} onClose={() => setDeleteOpen(false)} title={<Text fw={700}>Удаление</Text>}>
        <Stack>
          <Text size="md">
            Идентификатор записи: <span style={{ fontWeight: "bolder" }}>{id}</span>
          </Text>
          <Group>
            <Button
              variant="filled"
              color="red"
              radius="md"
              mt="xl"
              onClick={async () => {
                setLoading(true);
                const res = await programApi.delete(parseInt(id));
                setLoading(false);
                if (res != "ERROR") {
                  await navigate({ to: "/admin/program" });
                }
              }}
              loading={loading}>
              Удалить
            </Button>
            <Button variant="light" radius="md" mt="xl" onClick={() => setDeleteOpen(false)}>
              Отмена
            </Button>
          </Group>
        </Stack>
      </Modal>
    </AppShellAdmin>
  );
}

export { ProgramContentView };
