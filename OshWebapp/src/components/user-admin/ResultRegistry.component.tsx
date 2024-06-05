import { AppShellAdmin } from "./AppShellAdmin.component.tsx";
import { getQuery } from "../../api/crud.ts";
import { auth } from "../../api/api.ts";
import { Assigment } from "../../models/domain/Assigment.ts";
import { useEffect, useState } from "react";
import { SearchPageRes } from "../../models/SearchPageRes.ts";
// @ts-ignore
import { DataGrid, DataGridPaginationState } from "mantine-data-grid";
import { AppShell, Center, Group, Stack, Text, Title } from "@mantine/core";
import { ShieldAlert } from "lucide-react";
import { locale } from "../../utils/locale.tsx";

const api = getQuery<Assigment>("/api/osh-program-result", auth);

function ResultRegistry() {
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
            <Title order={1}>Результаты</Title>
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
    </AppShellAdmin>
  );
}

export { ResultRegistry };
