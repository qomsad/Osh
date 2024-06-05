import { AppShellAdmin } from "./AppShellAdmin.component.tsx";
import { getQuery } from "../../api/crud.ts";
import { auth } from "../../api/api.ts";
import { Assigment } from "../../models/domain/Assigment.ts";
import { useEffect, useState } from "react";
import { SearchPageRes } from "../../models/SearchPageRes.ts";
import { Employee } from "../../models/domain/Employee.ts";
// @ts-ignore
import { DataGrid, DataGridPaginationState } from "mantine-data-grid";
import { AppShell, Button, Center, Group, Modal, Select, Stack, Text, Title } from "@mantine/core";
import { Plus, ShieldAlert } from "lucide-react";
import { locale } from "../../utils/locale.tsx";
import { useForm } from "@mantine/form";
import { OshProgram } from "../../models/domain/OshProgram.ts";

const api = getQuery<Assigment>("/api/osh-program-assignment", auth);

function AssignmentRegistry() {
  const [addOpen, setAddOpen] = useState(false);

  const [loading, setLoading] = useState(false);
  const [data, setData] = useState<SearchPageRes<Assigment>>({
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
    <AppShellAdmin>
      <AppShell.Main>
        <Stack h="100%" align="flex-start" justify="center" gap="md" p={40}>
          <Group justify="space-between" w="100%">
            <Title order={1}>Назначения</Title>
            <Button rightSection={<Plus size={18} />} onClick={() => setAddOpen(true)}>
              Создать
            </Button>
          </Group>
          <DataGrid<SearchPageRes<Assigment>>
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
                size: 50,
              },
              {
                accessorFn: (row: any) => {
                  if (row.employee.lastName && row.employee.firstName && row.employee.middleName) {
                    return `${row.employee.lastName} ${row.employee.firstName} ${row.employee.middleName}`;
                  }
                  return `(${row.employee.login})`;
                },
                header: "Сотрудник",
                size: 300,
              },
              {
                accessorFn: (row: any) => {
                  return `${row.oshProgram.name}`;
                },
                header: "Программа обучения",
                size: 300,
              },
              {
                accessorKey: "assignmentDate",
                header: "Назначена",
                size: 150,
                //@ts-ignore
                cell: (cell) => new Date(cell?.getValue<Date>()).toLocaleString(),
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
              return {
                pageIndex: 0,
                pageSize: prev.pageSize,
              };
            });
            await load();
          }}
          onCancel={() => setAddOpen(false)}
        />
      </Modal>
    </AppShellAdmin>
  );
}

function AddAction({ onOk, onCancel }: { onOk: () => void; onCancel: () => void }) {
  const [loading, setLoading] = useState(false);
  const programApi = getQuery("/api/osh-program", auth);
  const employeeApi = getQuery("/api/user-employee", auth);
  const [selectProgram, setSelectProgram] = useState<{ value: string; label: string }[]>([]);
  const [selectEmployee, setSelectEmployee] = useState<{ value: string; label: string }[]>([]);

  const form = useForm({
    mode: "controlled",
    initialValues: {
      userEmployeeId: "",
      oshProgramId: "",
    },
  });

  const [searchProgram, setSearchProgram] = useState("");
  const [searchEmployee, setSearchEmployee] = useState("");

  const searchProgramFn = async () => {
    const res = await programApi.search(searchProgram, 0, 10);
    const content = res.content as OshProgram[];
    setSelectProgram(content.map((x) => ({ value: x.id.toString(), label: x.name })));
  };
  const searchEmployeeFn = async () => {
    const res = await employeeApi.search(searchEmployee, 0, 10);
    const content = res.content as Employee[];
    setSelectEmployee(
      content.map((x) => ({
        value: x.id.toString(),
        label: `${x.lastName} ${x.firstName} ${x.middleName}`,
      })),
    );
  };
  useEffect(() => {
    (async () => await searchProgramFn())();
  }, [searchProgram]);

  useEffect(() => {
    (async () => await searchEmployeeFn())();
  }, [searchEmployee]);

  return (
    <Stack>
      <form
        style={{ width: "100%" }}
        onSubmit={form.onSubmit(async (values) => {
          const val = {
            oshProgramId: parseInt(values.oshProgramId),
            userEmployeeId: parseInt(values.userEmployeeId),
          };
          setLoading(true);
          const res = await api.create({ ...val });
          setLoading(false);
          if (res != "ERROR") {
            onOk();
          }
        })}>
        <Select
          label="Сотрудник"
          w="100%"
          searchable
          searchValue={searchEmployee}
          onSearchChange={setSearchEmployee}
          data={selectEmployee}
          key={form.key("userEmployeeId")}
          {...form.getInputProps("userEmployeeId")}
        />

        <Select
          label="Программа обучения"
          w="100%"
          searchable
          searchValue={searchProgram}
          onSearchChange={setSearchProgram}
          data={selectProgram}
          key={form.key("oshProgramId")}
          {...form.getInputProps("oshProgramId")}
        />

        <Group>
          <Button variant="filled" color="green" radius="md" mt="xl" type="submit" loading={loading}>
            Назначить
          </Button>
          <Button variant="light" radius="md" mt="xl" onClick={onCancel}>
            Отмена
          </Button>
        </Group>
      </form>
    </Stack>
  );
}

export { AssignmentRegistry };
