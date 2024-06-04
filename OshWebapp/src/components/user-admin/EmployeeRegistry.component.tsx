import { AppShellAdmin } from "./AppShellAdmin.component.tsx";
import { getQuery } from "../../api/crud.ts";
import { Specialty } from "../../models/domain/Specialty.ts";
import { auth } from "../../api/api.ts";
import { useEffect, useState } from "react";
import { SearchPageRes } from "../../models/SearchPageRes.ts";
// @ts-ignore
import { DataGrid, DataGridPaginationState } from "mantine-data-grid";
import {
  ActionIcon,
  AppShell,
  Button,
  Center,
  Checkbox,
  Group,
  Modal,
  PasswordInput,
  Select,
  Stack,
  Text,
  TextInput,
  Title,
} from "@mantine/core";
import { Pencil, Plus, ShieldAlert, Trash2 } from "lucide-react";
import { locale } from "../../utils/locale.tsx";
import { isNotEmpty, matches, matchesField, useForm } from "@mantine/form";
import { Employee } from "../../models/domain/Employee.ts";
import { passGen } from "../../utils/pass_gen.tsx";
import { useDisclosure } from "@mantine/hooks";

const api = getQuery<Employee>("/api/user-employee", auth);

function EmployeeRegistry() {
  const [addOpen, setAddOpen] = useState(false);
  const [editOpen, setEditOpen] = useState(false);
  const [editId, setEditId] = useState(0);
  const [deleteOpen, setDeleteOpen] = useState(false);
  const [deleteId, setDeleteId] = useState(0);

  const [loading, setLoading] = useState(false);
  const [data, setData] = useState<SearchPageRes<Employee>>({
    content: [],
    totalCount: 0,
  });

  const [pagination, setPagination] = useState<DataGridPaginationState>({ pageIndex: 0, pageSize: 5 });
  const [searchValue, setSearchValue] = useState<string>("");

  async function load() {
    const res = await api.search(searchValue, pagination.pageIndex, pagination.pageSize);
    setData(res);
  }

  useEffect(() => {
    setLoading(true);
    (async () => load())();
    setLoading(false);
  }, [searchValue, pagination]);

  return (
    <AppShellAdmin>
      <AppShell.Main>
        <Stack h="100%" align="flex-start" justify="center" gap="md" p={40}>
          <Group justify="space-between" w="100%">
            <Title order={1}>Сотрудники</Title>
            <Button rightSection={<Plus size={18} />} onClick={() => setAddOpen(true)}>
              Создать
            </Button>
          </Group>
          <DataGrid<SearchPageRes<Employee>>
            style={{ width: "100%" }}
            data={data.content}
            total={data.totalCount}
            onPageChange={(prev: DataGridPaginationState) => {
              if (prev.pageSize != pagination.pageSize || prev.pageIndex != pagination.pageIndex) {
                setPagination(prev);
              }
            }}
            onSearch={(prev: string) => {
              setSearchValue(prev);
              setPagination({ pageIndex: 0, pageSize: pagination.pageSize });
            }}
            withPagination
            withGlobalFilter
            state={{ pagination }}
            loading={loading}
            locale={locale}
            empty={
              <Center>
                <Stack align="center">
                  <ShieldAlert size="100" />
                  <Text size="lg" fw="700">
                    Нет данных или авторизации
                  </Text>
                </Stack>
              </Center>
            }
            pageSizes={["5", "10", "25", "50"]}
            columns={[
              {
                accessorKey: "id",
                header: "#",
                size: 50,
              },
              {
                accessorKey: "login",
                header: "Логин",
                size: 80,
              },
              {
                accessorKey: "lastName",
                header: "Фамилия",
                size: 140,
              },
              {
                accessorKey: "firstName",
                header: "Имя",
                size: 140,
              },
              {
                accessorKey: "middleName",
                header: "Отчество",
                size: 140,
              },
              {
                accessorKey: "serviceNumber",
                header: "Табельный номер",
                size: 100,
              },
              {
                accessorKey: "specialty.name",
                header: "Специальность",
                size: 100,
              },
              {
                accessorKey: "edit",
                header: "Изменить",
                size: 53,
                //@ts-ignore
                cell: (cell) => {
                  return (
                    <ActionIcon
                      size="md"
                      color="orange"
                      onClick={() => {
                        setEditId(cell.row.original.id);
                        setEditOpen(true);
                      }}>
                      <Pencil size="15" />
                    </ActionIcon>
                  );
                },
              },
              {
                accessorKey: "delete",
                header: "Удалить",
                size: 45,
                //@ts-ignore
                cell: (cell) => {
                  return (
                    <ActionIcon
                      size="md"
                      color="red"
                      onClick={() => {
                        setDeleteId(cell.row.original.id);
                        setDeleteOpen(true);
                      }}>
                      <Trash2 size="15" />
                    </ActionIcon>
                  );
                },
              },
            ]}
          />
        </Stack>
      </AppShell.Main>
      <Modal opened={addOpen} onClose={() => setAddOpen(false)} title={<Text fw={700}>Создание</Text>}>
        <AddAction
          onOk={async () => {
            setAddOpen(false);
            setPagination((prev: DataGridPaginationState) => {
              const newTotalCount = data.totalCount + 1; // Общее количество элементов после добавления
              const newTotalPages = Math.ceil(newTotalCount / prev.pageSize); // Общее количество страниц после добавления
              const newPageIndex = Math.max(prev.pageIndex, newTotalPages - 1); // Корректировка текущей страницы
              return {
                pageIndex: newPageIndex,
                pageSize: prev.pageSize,
              };
            });
            await load();
          }}
          onCancel={() => setAddOpen(false)}
        />
      </Modal>
      <Modal opened={editOpen} onClose={() => setEditOpen(false)} title={<Text fw={700}>Изменение</Text>}>
        <EditAction
          id={editId}
          onOk={async () => {
            setEditOpen(false);
            await load();
          }}
          onCancel={() => setEditOpen(false)}
        />
      </Modal>
      <Modal opened={deleteOpen} onClose={() => setDeleteOpen(false)} title={<Text fw={700}>Удаление</Text>}>
        <DeleteAction
          id={deleteId}
          onOk={async () => {
            setDeleteOpen(false);
            setPagination((prev: DataGridPaginationState) => {
              const newTotalCount = data.totalCount - 1; // Общее количество элементов после удаления
              const newTotalPages = Math.ceil(newTotalCount / prev.pageSize); // Общее количество страниц после удаления
              const newPageIndex = Math.min(prev.pageIndex, newTotalPages - 1);
              return {
                pageIndex: newPageIndex,
                pageSize: prev.pageSize,
              };
            });
            await load();
          }}
          onCancel={() => setDeleteOpen(false)}
        />
      </Modal>
    </AppShellAdmin>
  );
}

function AddAction({ onOk, onCancel }: { onOk: () => void; onCancel: () => void }) {
  const [loading, setLoading] = useState(false);
  const specialtyApi = getQuery("/api/specialty", auth);
  const [select, setSelect] = useState<{ value: string; label: string }[]>([]);
  const [isButtonDisabled, setIsButtonDisabled] = useState(false);
  const [visible, { toggle }] = useDisclosure(false);

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      login: "",
      firstName: "",
      middleName: "",
      lastName: "",
      serviceNumber: "",
      specialtyId: "",
      password: "",
      passwordC: "",
    },
    validate: {
      login: matches(/^[a-z0-9_]{3,}$/, "Логин должен содержать только английские символы, цифры и '_'"),
      firstName: isNotEmpty("Имя должно быть заполнено"),
      lastName: isNotEmpty("Фамилия должна быть заполнена"),
      specialtyId: isNotEmpty("Специальность должна быть заполнена"),
      password: matches(/^[!-~]{6,}$/, "Пароль должен быть 6 символов"),
      passwordC: matchesField("password", "Пароли должны совпадать"),
    },
  });
  useEffect(() => {
    setIsButtonDisabled(!form.isTouched());
  }, [form, form.isTouched]);

  const [searchV, setSearchV] = useState("");
  const search = async () => {
    const res = await specialtyApi.search(searchV, 0, 10);
    const content = res.content as Specialty[];
    setSelect(content.map((x) => ({ value: x.id.toString(), label: x.name })));
  };
  useEffect(() => {
    (async () => await search())();
  }, [searchV]);

  return (
    <Stack>
      <form
        style={{ width: "100%" }}
        onSubmit={form.onSubmit(
          async (values) => {
            const val = {
              ...values,
              specialtyId: parseInt(values.specialtyId),
              serviceNumber: values.serviceNumber != "" ? values.serviceNumber : undefined,
            };
            setLoading(true);
            const res = await api.create({ ...val });
            setLoading(false);
            if (res != "ERROR") {
              onOk();
            }
          },
          () => {
            form.resetTouched();
            setIsButtonDisabled(true);
          },
        )}>
        <TextInput label="Логин" w="100%" key={form.key("login")} {...form.getInputProps("login")} />
        <TextInput label="Фамилия" w="100%" key={form.key("lastName")} {...form.getInputProps("lastName")} />
        <TextInput label="Имя" w="100%" key={form.key("firstName")} {...form.getInputProps("firstName")} />
        <TextInput label="Отчество" w="100%" key={form.key("middleName")} {...form.getInputProps("middleName")} />
        <TextInput
          label="Табельный номер"
          w="100%"
          key={form.key("serviceNumber")}
          {...form.getInputProps("serviceNumber")}
        />
        <Select
          label="Специальность"
          w="100%"
          searchable
          searchValue={searchV}
          onSearchChange={setSearchV}
          data={select}
          key={form.key("specialtyId")}
          {...form.getInputProps("specialtyId")}
        />
        <PasswordInput
          label="Пароль"
          w="100%"
          key={form.key("password")}
          {...form.getInputProps("password")}
          visible={visible}
          onVisibilityChange={toggle}
        />
        <Button
          mt="xs"
          size="compact-xs"
          onClick={() => {
            const pass = passGen();
            form.setFieldValue("password", pass);
            form.setFieldValue("passwordC", pass);
            toggle();
          }}>
          Сгенерировать
        </Button>
        <PasswordInput
          label="Подтверждение пароля"
          w="100%"
          key={form.key("passwordC")}
          {...form.getInputProps("passwordC")}
          visible={visible}
          onVisibilityChange={toggle}
        />

        <Group>
          <Button
            variant="filled"
            color="green"
            radius="md"
            mt="xl"
            type="submit"
            loading={loading}
            disabled={isButtonDisabled}>
            {isButtonDisabled ? <span style={{ color: "red" }}>Ошибка</span> : <span>Создать</span>}
          </Button>
          <Button variant="light" radius="md" mt="xl" onClick={onCancel}>
            Отмена
          </Button>
        </Group>
      </form>
    </Stack>
  );
}

// @ts-ignore
// eslint-disable-next-line @typescript-eslint/no-unused-vars
function EditAction({ id, onOk, onCancel }: { id: number; onOk: () => void; onCancel: () => void }) {
  const [loading, setLoading] = useState(false);
  const specialtyApi = getQuery("/api/specialty", auth);
  const [select, setSelect] = useState<{ value: string; label: string }[]>([]);
  const [isButtonDisabled, setIsButtonDisabled] = useState(false);
  const [visible, { toggle }] = useDisclosure(false);
  const [isUrlCheck, setIsUrlCheck] = useState(false);
  const [login, setLogin] = useState("");

  const [searchV, setSearchV] = useState("");
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
      login: "",
      firstName: "",
      middleName: "",
      lastName: "",
      serviceNumber: "",
      specialtyId: "",
      password: "",
      passwordC: "",
    },
    validate: {
      login: matches(/^[a-z0-9_]{3,}$/, "Логин должен содержать только английские символы, цифры и '_'"),
      firstName: isNotEmpty("Имя должно быть заполнено"),
      lastName: isNotEmpty("Фамилия должна быть заполнена"),
      specialtyId: isNotEmpty("Специальность должна быть заполнена"),
      password: (() => {
        if (isUrlCheck) {
          return matches(/^[!-~]{6,}$/, "Пароль должен быть 6 символов");
        }
      })(),
      passwordC: (() => {
        if (isUrlCheck) {
          return matchesField("password", "Пароли должны совпадать");
        }
      })(),
    },
  });
  useEffect(() => {
    setIsButtonDisabled(!form.isTouched());
  }, [form, form.isTouched]);

  useEffect(() => {
    api.getById(id).then((value) => {
      if (value) {
        form.setFieldValue("login", value.login);
        form.setFieldValue("lastName", value.lastName);
        form.setFieldValue("firstName", value.firstName);
        form.setFieldValue("middleName", value.middleName);
        form.setFieldValue("serviceNumber", value.serviceNumber);
        form.setFieldValue("specialtyId", value.specialty.id.toString());
        setLogin(value.login);
      }
      form.resetDirty();
    });
  }, []);

  return (
    <Stack>
      <Text size="md">
        Логин: <span style={{ fontWeight: "bolder" }}>{login}</span>
      </Text>
      <form
        style={{ width: "100%" }}
        onSubmit={form.onSubmit(
          async (values) => {
            const val = {
              ...values,
              specialtyId: parseInt(values.specialtyId),
              serviceNumber: values.serviceNumber != "" ? values.serviceNumber : undefined,
              password: values.password != "" ? values.password : undefined,
              passwordC: values.passwordC != "" ? values.passwordC : undefined,
            };
            setLoading(true);
            const res = await api.update(id, { ...val });
            setLoading(false);
            if (res != "ERROR") {
              onOk();
            }
          },
          () => {
            form.resetTouched();
            setIsButtonDisabled(true);
          },
        )}>
        <TextInput label="Фамилия" w="100%" key={form.key("lastName")} {...form.getInputProps("lastName")} />
        <TextInput label="Имя" w="100%" key={form.key("firstName")} {...form.getInputProps("firstName")} />
        <TextInput label="Отчество" w="100%" key={form.key("middleName")} {...form.getInputProps("middleName")} />
        <TextInput
          label="Табельный номер"
          w="100%"
          key={form.key("serviceNumber")}
          {...form.getInputProps("serviceNumber")}
        />
        <Select
          label="Специальность"
          w="100%"
          searchable
          onSearchChange={setSearchV}
          data={select}
          key={form.key("specialtyId")}
          {...form.getInputProps("specialtyId")}
        />
        <Checkbox
          checked={isUrlCheck}
          mt="xs"
          onChange={() => {
            setIsUrlCheck(!isUrlCheck);
          }}
          label="Изменить пароль"
        />
        {isUrlCheck && (
          <>
            <PasswordInput
              label="Пароль"
              w="100%"
              key={form.key("password")}
              {...form.getInputProps("password")}
              visible={visible}
              onVisibilityChange={toggle}
              disabled={!isUrlCheck}
            />
            <Button
              mt="xs"
              size="compact-xs"
              onClick={() => {
                const pass = passGen();
                form.setFieldValue("password", pass);
                form.setFieldValue("passwordC", pass);
                toggle();
              }}>
              Сгенерировать
            </Button>
            <PasswordInput
              label="Подтверждение пароля"
              w="100%"
              key={form.key("passwordC")}
              {...form.getInputProps("passwordC")}
              visible={visible}
              onVisibilityChange={toggle}
              disabled={!isUrlCheck}
            />
          </>
        )}

        <Group>
          <Button
            variant="filled"
            color="orange"
            radius="md"
            mt="xl"
            type="submit"
            loading={loading}
            disabled={isButtonDisabled}>
            {isButtonDisabled ? <span style={{ color: "red" }}>Ошибка</span> : <span>Изменить</span>}
          </Button>
          <Button variant="light" radius="md" mt="xl" onClick={onCancel}>
            Отмена
          </Button>
        </Group>
      </form>
    </Stack>
  );
}

function DeleteAction({ id, onOk, onCancel }: { id: number; onOk: () => void; onCancel: () => void }) {
  const [loading, setLoading] = useState(false);

  return (
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
            const res = await api.delete(id);
            setLoading(false);
            if (res != "ERROR") {
              onOk();
            }
          }}
          loading={loading}>
          Удалить
        </Button>
        <Button variant="light" radius="md" mt="xl" onClick={onCancel}>
          Отмена
        </Button>
      </Group>
    </Stack>
  );
}

export { EmployeeRegistry };
