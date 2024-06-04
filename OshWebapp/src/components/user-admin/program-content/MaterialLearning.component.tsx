import { Api, getQuery } from "../../../api/crud.ts";
import { auth, authFileSend } from "../../../api/api.ts";
import { SearchPageRes } from "../../../models/SearchPageRes.ts";
// @ts-ignore
import { DataGrid, DataGridPaginationState } from "mantine-data-grid";
import { locale } from "../../../utils/locale.tsx";
import {
  ActionIcon,
  Button,
  Center,
  Checkbox,
  FileInput,
  Group,
  Modal,
  Stack,
  Text,
  Textarea,
  TextInput,
} from "@mantine/core";
import { CloudUpload, Eye, Pencil, Plus, ShieldAlert, Trash2 } from "lucide-react";
import { Learning } from "../../../models/domain/Learning.tsx";
import { useEffect, useState } from "react";
import { useForm } from "@mantine/form";
import { ErrorHandler } from "../../../api/ErrorHadler.ts";
import { PreviewMaterial } from "../material-content/PreviewMaterial.component.tsx";

interface MaterialLearningProps {
  programId: number;
}

function MaterialLearning({ programId }: MaterialLearningProps) {
  const api = getQuery<Learning>(`/api/osh-program/${programId}/learning`, auth);

  const [addOpen, setAddOpen] = useState(false);
  const [editOpen, setEditOpen] = useState(false);
  const [editId, setEditId] = useState(0);
  const [deleteOpen, setDeleteOpen] = useState(false);
  const [deleteId, setDeleteId] = useState(0);

  const [previewOpen, setPrewievOpen] = useState(false);
  const [previewId, setPreviewId] = useState(0);

  const [loading, setLoading] = useState(false);
  const [data, setData] = useState<SearchPageRes<Learning>>({
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

      <DataGrid<SearchPageRes<Learning>>
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
            size: 60,
          },
          {
            accessorKey: "name",
            header: "Название",
            size: 300,
          },
          {
            accessorFn: (row: any) => `${row.learningSectionFile ? "Файл" : "Текст"}`,
            header: "Тип",
            size: 100,
          },
          {
            accessorKey: "open",
            header: "Открыть",
            size: 53,
            //@ts-ignore
            cell: (cell) => {
              return (
                <ActionIcon
                  size="md"
                  color="dark"
                  onClick={() => {
                    setPreviewId(cell.row.original.id);
                    setPrewievOpen(true);
                  }}>
                  <Eye size="15" />
                </ActionIcon>
              );
            },
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
      <Modal
        opened={previewOpen}
        onClose={() => setPrewievOpen(false)}
        title={<Text fw={700}>Просмотр</Text>}
        fullScreen>
        <PreviewMaterial api={api} learningId={previewId} />
      </Modal>
      <Modal
        opened={addOpen}
        onClose={() => setAddOpen(false)}
        title={<Text fw={700}>Добавление обучающего материала</Text>}>
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
          api={api}
        />
      </Modal>
      <Modal
        opened={editOpen}
        onClose={() => setEditOpen(false)}
        title={<Text fw={700}>Изменение обучающего материала</Text>}>
        <EditAction
          id={editId}
          onOk={async () => {
            setEditOpen(false);
            await load();
          }}
          onCancel={() => setEditOpen(false)}
          api={api}
        />
      </Modal>
      <Modal
        opened={deleteOpen}
        onClose={() => setDeleteOpen(false)}
        title={<Text fw={700}>Удаление обучающего материала</Text>}>
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

function AddAction({ onOk, onCancel, api }: { onOk: () => void; onCancel: () => void; api: Api<Learning> }) {
  const [loading, setLoading] = useState(false);

  const [file, setFile] = useState<File | null>(null);

  const form = useForm({
    mode: "controlled",
    initialValues: {
      name: "",
      text: "",
    },
  });

  return (
    <Stack>
      <form
        style={{ width: "100%" }}
        onSubmit={form.onSubmit(async (values) => {
          setLoading(true);
          if (file) {
            const byte = await file.arrayBuffer();

            const form = new FormData();
            form.append("file", new Blob([byte]));
            const res = await authFileSend().post("/api/s3", form).catch(ErrorHandler);
            if (res?.data && res.data.id) {
              const val = { ...values, learningSectionFile: { fileType: "Pdf", filePath: res.data.id } };
              const data = await api.create(val);
              if (data != "ERROR") {
                onOk();
              }
            }
          }
          setLoading(false);
        })}>
        <TextInput label="Название" w="100%" key={form.key("name")} {...form.getInputProps("name")} />
        <Textarea
          label="Текст"
          placeholder="Текст который будет отображатся до просмотра файла"
          key={form.key("text")}
          {...form.getInputProps("text")}
        />
        <FileInput
          leftSection={<CloudUpload />}
          label="Загрузка файла"
          placeholder="Открыть"
          leftSectionPointerEvents="none"
          accept="application/pdf"
          value={file}
          onChange={setFile}
        />
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

function EditAction({
  id,
  onOk,
  onCancel,
  api,
}: {
  id: number;
  onOk: () => void;
  onCancel: () => void;
  api: Api<Learning>;
}) {
  const [loading, setLoading] = useState(false);

  const [file, setFile] = useState<File | null>(null);
  const [isUrlCheck, setIsUrlCheck] = useState(false);
  const [oldFile, setOldFile] = useState("");

  const form = useForm({
    mode: "controlled",
    initialValues: {
      name: "",
      text: "",
    },
  });

  useEffect(() => {
    api.getById(id).then((value) => {
      if (value) {
        form.setFieldValue("name", value.name);
        form.setFieldValue("text", value.text);
        if (value?.learningSectionFile && value.learningSectionFile?.filePath) {
          setOldFile(value.learningSectionFile.filePath);
        }
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
          if (file) {
            const byte = await file.arrayBuffer();

            const form = new FormData();
            form.append("file", new Blob([byte]));
            const res = await authFileSend().post("/api/s3", form).catch(ErrorHandler);
            if (res?.data && res.data.id) {
              const val = { ...values, learningSectionFile: { fileType: "Pdf", filePath: res.data.id } };
              const data = await api.update(id, val);
              if (data != "ERROR") {
                onOk();
              }
            }
          } else {
            let val;
            if (oldFile !== "") {
              val = { ...values, learningSectionFile: { fileType: "Pdf", filePath: oldFile } };
            } else {
              val = { ...values };
            }
            const data = await api.update(id, val);
            if (data != "ERROR") {
              onOk();
            }
          }
          setLoading(false);
        })}>
        <TextInput label="Название" w="100%" key={form.key("name")} {...form.getInputProps("name")} />
        <Textarea
          label="Текст"
          placeholder="Текст который будет отображатся до просмотра файла"
          key={form.key("text")}
          {...form.getInputProps("text")}
        />
        <Checkbox
          checked={isUrlCheck}
          mt="xs"
          onChange={() => {
            setIsUrlCheck(!isUrlCheck);
          }}
          label="Изменить файл"
        />
        {isUrlCheck && (
          <FileInput
            leftSection={<CloudUpload />}
            label="Загрузка файла"
            placeholder="Открыть"
            leftSectionPointerEvents="none"
            accept="application/pdf"
            value={file}
            onChange={setFile}
          />
        )}
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

function DeleteAction({
  id,
  onOk,
  onCancel,
  api,
}: {
  id: number;
  onOk: () => void;
  onCancel: () => void;
  api: Api<Learning>;
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

export { MaterialLearning };
