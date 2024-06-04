import { AppShellAdmin } from "./AppShellAdmin.component.tsx";
// @ts-ignore
import { DataGrid, DataGridPaginationState } from "mantine-data-grid";
import { useEffect, useState } from "react";
import { SearchPageRes } from "../../models/SearchPageRes.ts";
import { Specialty } from "../../models/domain/Specialty.ts";
import { ActionIcon, AppShell, Button, Center, Group, Modal, Stack, Text, TextInput, Title } from "@mantine/core";
import { locale } from "../../utils/locale.tsx";
import { Pencil, Plus, ShieldAlert, Trash2 } from "lucide-react";
import { useForm } from "@mantine/form";
import { getQuery } from "../../api/crud.ts";
import { auth } from "../../api/api.ts";

const api = getQuery<Specialty>("/api/specialty", auth);

function SpecialtyRegistry() {
  const [addOpen, setAddOpen] = useState(false);
  const [editOpen, setEditOpen] = useState(false);
  const [editId, setEditId] = useState(0);
  const [deleteOpen, setDeleteOpen] = useState(false);
  const [deleteId, setDeleteId] = useState(0);

  const [loading, setLoading] = useState(false);
  const [data, setData] = useState<SearchPageRes<Specialty>>({
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
            <Title order={1}>Специальности</Title>
            <Button rightSection={<Plus size={18} />} onClick={() => setAddOpen(true)}>
              Создать
            </Button>
          </Group>
          <DataGrid<SearchPageRes<Specialty>>
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
                size: 60,
              },
              {
                accessorKey: "name",
                header: "Название",
                size: 300,
              },
              {
                accessorKey: "created",
                header: "Создана",
                size: 150,
                //@ts-ignore
                cell: (cell) => new Date(cell?.getValue<Date>()).toLocaleString(),
              },
              {
                accessorKey: "updated",
                header: "Изменена",
                size: 150,
                //@ts-ignore
                cell: (cell) => new Date(cell?.getValue<Date>()).toLocaleString(),
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

  const form = useForm({
    mode: "controlled",
    initialValues: {
      name: "",
    },
  });

  return (
    <Stack>
      <form
        style={{ width: "100%" }}
        onSubmit={form.onSubmit(async (values) => {
          setLoading(true);
          const res = await api.create({ ...values });
          setLoading(false);
          if (res != "ERROR") {
            onOk();
          }
        })}>
        <TextInput label="Название" w="100%" key={form.key("name")} {...form.getInputProps("name")} />
        <Group>
          <Button variant="filled" color="green" radius="md" mt="xl" type="submit" loading={loading}>
            Создать
          </Button>
          <Button variant="light" radius="md" mt="xl" onClick={onCancel}>
            Отмена
          </Button>
        </Group>
      </form>
    </Stack>
  );
}

function EditAction({ id, onOk, onCancel }: { id: number; onOk: () => void; onCancel: () => void }) {
  const [loading, setLoading] = useState(false);

  const form = useForm({
    mode: "controlled",
    initialValues: {
      name: "",
    },
  });

  useEffect(() => {
    api.getById(id).then((value) => {
      if (value) {
        form.setFieldValue("name", value.name);
      }
      form.resetDirty();
    });
  }, []);

  return (
    <Stack>
      <Text size="md">
        Идентификатор записи: <span style={{ fontWeight: "bolder" }}>{id}</span>
      </Text>
      <form
        style={{ width: "100%" }}
        onSubmit={form.onSubmit(async (values) => {
          setLoading(true);
          const res = await api.update(id, { ...values });
          setLoading(false);
          if (res != "ERROR") {
            onOk();
          }
        })}>
        <TextInput label="Название" w="100%" key={form.key("name")} {...form.getInputProps("name")} />
        <Group>
          <Button variant="filled" color="orange" radius="md" mt="xl" type="submit" loading={loading}>
            Изменить
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

export { SpecialtyRegistry };
