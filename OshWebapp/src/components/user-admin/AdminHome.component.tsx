import { AppShellAdmin } from "./AppShellAdmin.component.tsx";
import { AppShell, Card, Group, SimpleGrid, Stack, Text, Title, UnstyledButton } from "@mantine/core";
import { BookCheck, BookOpen, CheckCheck, Hammer, UserRoundSearch } from "lucide-react";
import { Link } from "@tanstack/react-router";

function AdminHome() {
  return (
    <AppShellAdmin>
      <AppShell.Main>
        <Stack h="100%" align="flex-start" justify="center" gap="md" p={40}>
          <Title order={1}>Панель администратора</Title>
          <SimpleGrid cols={4}>
            <UnstyledButton component={Link} to="/admin/specialty">
              <Card shadow="sm" padding="lg" radius="md" withBorder>
                <Group justify="start" align="center" mb="xs">
                  <Hammer size={50} />
                  <Text size="lg" fw={500}>
                    Специальности
                  </Text>
                </Group>
              </Card>
            </UnstyledButton>
            <UnstyledButton component={Link} to="/admin/employee">
              <Card shadow="sm" padding="lg" radius="md" withBorder>
                <Group justify="start" align="center" mb="xs">
                  <UserRoundSearch size={50} />
                  <Text size="lg" fw={500}>
                    Сотрудники
                  </Text>
                </Group>
              </Card>
            </UnstyledButton>
            <UnstyledButton component={Link} to="/admin/program">
              <Card shadow="sm" padding="lg" radius="md" withBorder>
                <Group justify="start" align="center" mb="xs">
                  <BookOpen size={50} />
                  <Text size="lg" fw={500}>
                    Программы обучения
                  </Text>
                </Group>
              </Card>
            </UnstyledButton>
            <UnstyledButton component={Link} to="/admin/assignment">
              <Card shadow="sm" padding="lg" radius="md" withBorder>
                <Group justify="start" align="center" mb="xs">
                  <BookCheck size={50} />
                  <Text size="lg" fw={500}>
                    Назначения
                  </Text>
                </Group>
              </Card>
            </UnstyledButton>
            <UnstyledButton component={Link} to="/admin/result">
              <Card shadow="sm" padding="lg" radius="md" withBorder>
                <Group justify="start" align="center" mb="xs">
                  <CheckCheck size={50} />
                  <Text size="lg" fw={500}>
                    Результаты
                  </Text>
                </Group>
              </Card>
            </UnstyledButton>
          </SimpleGrid>
        </Stack>
      </AppShell.Main>
    </AppShellAdmin>
  );
}

export { AdminHome };
