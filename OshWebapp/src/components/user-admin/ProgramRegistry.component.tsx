import { AppShellAdmin } from "./AppShellAdmin.component.tsx";
import { getQuery } from "../../api/crud.ts";
import { auth } from "../../api/api.ts";
import { useEffect, useState } from "react";
import { SearchPageRes } from "../../models/SearchPageRes.ts";
// @ts-ignore
import { DataGrid, DataGridPaginationState } from "mantine-data-grid";
import { ActionIcon, AppShell, Button, Center, Group, Stack, Text, Title } from "@mantine/core";
import { Plus, ShieldAlert, SquareArrowOutUpRight } from "lucide-react";
import { locale } from "../../utils/locale.tsx";
import { OshProgram } from "../../models/domain/OshProgram.ts";
import { useNavigate } from "@tanstack/react-router";

const api = getQuery<OshProgram>("/api/osh-program", auth);

function ProgramRegistry() {
  const [loading, setLoading] = useState(false);
  const [data, setData] = useState<SearchPageRes<OshProgram>>({
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

  const navigate = useNavigate();

  return (
    <AppShellAdmin>
      <AppShell.Main>
        <Stack h="100%" align="flex-start" justify="center" gap="md" p={40}>
          <Group justify="space-between" w="100%">
            <Title order={1}>Программы обучения</Title>
            <Button
              rightSection={<Plus size={18} />}
              onClick={() =>
                navigate({
                  to: `/admin/program/create`,
                  resetScroll: true,
                })
              }>
              Создать
            </Button>
          </Group>
          <DataGrid<SearchPageRes<OshProgram>>
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
                accessorFn: (row: any) => `${row.specialty?.name ?? ""}`,
                header: "Специальность",
                size: 300,
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
                      variant="light"
                      onClick={() =>
                        navigate({
                          to: `/admin/program/${cell.row.original.id}`,
                          resetScroll: true,
                        })
                      }>
                      <SquareArrowOutUpRight size="15" />
                    </ActionIcon>
                  );
                },
              },
            ]}
          />
        </Stack>
      </AppShell.Main>
    </AppShellAdmin>
  );
}

export { ProgramRegistry };
