import { Api, getQuery } from "../../../api/crud.ts";
import { auth } from "../../../api/api.ts";
import { useEffect, useState } from "react";
import { SearchPageRes } from "../../../models/SearchPageRes.ts";
// @ts-ignore
import { DataGrid, DataGridPaginationState } from "mantine-data-grid";
import { ActionIcon, Button, Center, Group, Modal, Stack, Text } from "@mantine/core";
import { Eye, Plus, ShieldAlert, Trash2 } from "lucide-react";
import { locale } from "../../../utils/locale.tsx";
import { Training } from "../../../models/domain/Training.ts";
import { ViewQuestion } from "../material-question/ViewQuestion.component.tsx";
import { CreateQuestion } from "../material-question/CreateQuestion.component.tsx";

interface MaterialTrainingProps {
  programId: number;
}

function MaterialTraining({ programId }: MaterialTrainingProps) {
  const api = getQuery<Training>(`/api/osh-program/${programId}/question`, auth);

  const [addOpen, setAddOpen] = useState(false);
  const [editOpen, setEditOpen] = useState(false);
  const [editId, setEditId] = useState(0);
  const [deleteOpen, setDeleteOpen] = useState(false);
  const [deleteId, setDeleteId] = useState(0);

  const [loading, setLoading] = useState(false);
  const [data, setData] = useState<SearchPageRes<Training>>({
    content: [],
    totalCount: 0,
  });

  const [pagination, setPagination] = useState<DataGridPaginationState>({ pageIndex: 0, pageSize: 5 });

  async function load() {
    const res = await api.search("", pagination.pageIndex, pagination.pageSize);
    setData(res);
  }

  useEffect(() => {
    setLoading(true);
    (async () => load())();
    setLoading(false);
  }, [pagination]);
  return (
    <Stack>
      <Group justify="end">
        <Button rightSection={<Plus size={18} />} onClick={() => setAddOpen(true)}>
          Создать
        </Button>
      </Group>

      <DataGrid<SearchPageRes<Training>>
        style={{ width: "100%" }}
        data={data.content}
        total={data.totalCount}
        onPageChange={(prev: DataGridPaginationState) => {
          if (prev.pageSize != pagination.pageSize || prev.pageIndex != pagination.pageIndex) {
            setPagination(prev);
          }
        }}
        withPagination
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
            size: 20,
          },
          {
            accessorKey: "question",
            header: "Вопрос",
            size: 300,
            //@ts-ignore
            cell: (cell) => {
              return <div style={{ textWrap: "wrap" }}>{cell.row.original.question}</div>;
            },
          },
          {
            accessorKey: "edit",
            header: "Открыть",
            size: 53,
            //@ts-ignore
            cell: (cell) => {
              return (
                <ActionIcon
                  size="md"
                  color="dark"
                  onClick={() => {
                    setEditId(cell.row.original.id);
                    setEditOpen(true);
                  }}>
                  <Eye size="15" />
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
      <Modal
        opened={addOpen}
        onClose={() => setAddOpen(false)}
        title={<Text fw={700}>Добавление вопроса</Text>}
        size="lg">
        <CreateQuestion
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
          api={api}
        />
      </Modal>
      <Modal opened={editOpen} onClose={() => setEditOpen(false)} title={<Text fw={700}>Просмотр вопроса</Text>}>
        <ViewQuestion id={editId} api={api} />
      </Modal>
      <Modal opened={deleteOpen} onClose={() => setDeleteOpen(false)} title={<Text fw={700}>Удаление вопроса</Text>}>
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
          api={api}
        />
      </Modal>
    </Stack>
  );
}

function DeleteAction({
  id,
  onOk,
  onCancel,
  api,
}: {
  id: number;
  onOk: () => void;
  onCancel: () => void;
  api: Api<Training>;
}) {
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

export { MaterialTraining };
