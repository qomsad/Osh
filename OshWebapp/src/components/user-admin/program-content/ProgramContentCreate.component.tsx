import { AppShell, Button, Card, Grid, Select, Stack, Text, TextInput, Title } from "@mantine/core";
import { AppShellAdmin } from "../AppShellAdmin.component.tsx";
import { isNotEmpty, matches, useForm } from "@mantine/form";
import { useEffect, useState } from "react";
import { Specialty } from "../../../models/domain/Specialty.ts";
import { getQuery } from "../../../api/crud.ts";
import { auth } from "../../../api/api.ts";
import { useNavigate } from "@tanstack/react-router";

const specialtyApi = getQuery("/api/specialty", auth);
const api = getQuery("/api/osh-program", auth);

function ProgramContentCreate() {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [select, setSelect] = useState<{ value: string; label: string }[]>([]);
  const [searchV, setSearchV] = useState("");
  useEffect(() => {
    (async () => await search())();
  }, [searchV]);
  const search = async () => {
    const res = await specialtyApi.search(searchV, 0, 10);
    const content = res.content as Specialty[];
    setSelect(content.map((x) => ({ value: x.id.toString(), label: x.name })));
  };

  const [isButtonDisabled, setIsButtonDisabled] = useState(false);
  const form = useForm({
    mode: "controlled",
    initialValues: {
      name: "",
      description: "",
      learningMinutesDuration: "",
      trainingMinutesDuration: "",
      specialtyId: "",
      trainingSuccessRate: "",
    },
    validate: {
      name: isNotEmpty("Название не может быть пустым"),
      learningMinutesDuration: matches(/^[1-9]\d*$/, "Целое число больше 0"),
      trainingMinutesDuration: matches(/^[1-9]\d*$/, "Целое число больше 0"),
      specialtyId: isNotEmpty("Специальность должна быть заполнена"),
      trainingSuccessRate: matches(
        /^(?:100(?:\.0{1,2})?|\d{1,2}(?:\.\d{1,2})?)$/,
        "Число процентов (от 0.00 до 100.00)",
      ),
    },
  });
  useEffect(() => {
    setIsButtonDisabled(!form.isTouched());
  }, [form, form.isTouched]);

  return (
    <AppShellAdmin>
      <AppShell.Main>
        <Grid>
          <Grid.Col span={8}>
            <Stack h="100%" align="flex-start" justify="center" gap="md" p={40}>
              <Title order={1}>Создание программы обучения</Title>
              <form
                style={{ width: "100%" }}
                onSubmit={form.onSubmit(
                  async (values) => {
                    const val = {
                      ...values,
                      learningMinutesDuration: parseInt(values.learningMinutesDuration),
                      trainingMinutesDuration: parseInt(values.trainingMinutesDuration),
                      specialityId: parseInt(values.specialtyId),
                      autoAssignmentType: "FullManual",
                      trainingSuccessRate: parseFloat(values.trainingSuccessRate),
                    };
                    setLoading(true);
                    const res = await api.create({ ...val });
                    setLoading(false);
                    if (res != "ERROR") {
                      await navigate({ to: "/admin/program", resetScroll: true });
                    }
                  },
                  () => {
                    form.resetTouched();
                    setIsButtonDisabled(true);
                  },
                )}>
                <Card shadow="sm" padding="lg" radius="md" withBorder>
                  <TextInput
                    size="sm"
                    label="Название"
                    w="100%"
                    labelProps={{ required: true, style: { fontSize: "25px" } }}
                    key={form.key("name")}
                    {...form.getInputProps("name")}
                  />
                  <Text size="sm" c="dimmed">
                    Название программы, используется для поиска
                  </Text>

                  <TextInput
                    size="sm"
                    label="Описание"
                    w="100%"
                    mt="md"
                    labelProps={{ style: { fontSize: "25px" } }}
                    key={form.key("description")}
                    {...form.getInputProps("description")}
                  />

                  <TextInput
                    size="sm"
                    label="Продолжительность обучения"
                    w="100%"
                    mt="md"
                    labelProps={{ required: true, style: { fontSize: "25px" } }}
                    key={form.key("learningMinutesDuration")}
                    {...form.getInputProps("learningMinutesDuration")}
                  />
                  <Text size="sm" c="dimmed">
                    Количество минут, требуемое на изучение материала
                  </Text>

                  <TextInput
                    size="sm"
                    label="Продолжительность тестирования"
                    w="100%"
                    mt="md"
                    labelProps={{ required: true, style: { fontSize: "25px" } }}
                    key={form.key("trainingMinutesDuration")}
                    {...form.getInputProps("trainingMinutesDuration")}
                  />
                  <Text size="sm" c="dimmed">
                    Максимальное время на решение теста в минутах
                  </Text>

                  <Select
                    label="Специальность"
                    labelProps={{ required: true, style: { fontSize: "25px" } }}
                    w="100%"
                    mt="md"
                    searchable
                    searchValue={searchV}
                    onSearchChange={setSearchV}
                    data={select}
                    key={form.key("specialtyId")}
                    {...form.getInputProps("specialtyId")}
                  />

                  <TextInput
                    size="sm"
                    label="Процент успеха теста"
                    w="100%"
                    mt="md"
                    labelProps={{ required: true, style: { fontSize: "25px" } }}
                    key={form.key("trainingSuccessRate")}
                    {...form.getInputProps("trainingSuccessRate")}
                  />
                  <Text size="sm" c="dimmed">
                    Соотношение количества правильных вопросов, не правильным
                  </Text>

                  <Button
                    color="blue"
                    fullWidth
                    radius="md"
                    mt="xl"
                    type="submit"
                    disabled={isButtonDisabled}
                    loading={loading}>
                    {isButtonDisabled ? (
                      <span style={{ color: "red" }}>Проверьте правильность заполнения полей</span>
                    ) : (
                      <span>Создать программу обучения</span>
                    )}
                  </Button>
                </Card>
              </form>
            </Stack>
          </Grid.Col>
        </Grid>
      </AppShell.Main>
    </AppShellAdmin>
  );
}

export { ProgramContentCreate };
