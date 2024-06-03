import { AppShell, Button, Grid, Group, Text, UnstyledButton } from "@mantine/core";
import { LogOut, ShieldQuestion } from "lucide-react";
import React from "react";
import { Link, useNavigate, useRouterState } from "@tanstack/react-router";
import { exit, getName } from "../../api/api.ts";

interface AppShellOpenProps {
  children?: React.ReactNode;
  head?: string;
}

function AppShellAdmin({ children }: AppShellOpenProps) {
  const router = useRouterState();
  const navigate = useNavigate();

  return (
    <AppShell header={{ height: 60 }} footer={{ height: 60 }} padding="md">
      <AppShell.Header>
        <Group h="100%" px="md" justify="space-between">
          <Group h="100%">
            <ShieldQuestion size={30} />
            <Text size="lg" fw={500} component={Link} to="/admin">
              АИС Охрана труда
            </Text>
            <Group h="100%" px="md" align="center">
              <UnstyledButton component={Link} to="/admin/specialty">
                <Text fw={router.location.href.startsWith("/admin/specialty") ? 700 : undefined}>Специальности</Text>
              </UnstyledButton>
              <UnstyledButton component={Link} to="/admin/employee">
                <Text fw={router.location.href.startsWith("/admin/employee") ? 700 : undefined}>Сотрудники</Text>
              </UnstyledButton>
              <UnstyledButton component={Link} to="/admin/program">
                <Text fw={router.location.href.startsWith("/admin/program") ? 700 : undefined}>Программы обучения</Text>
              </UnstyledButton>
              <UnstyledButton component={Link} to="/admin/assignment">
                <Text fw={router.location.href.startsWith("/admin/assignment") ? 700 : undefined}>Назначения</Text>
              </UnstyledButton>
              <UnstyledButton component={Link} to="/admin/result">
                <Text fw={router.location.href.startsWith("/admin/result") ? 700 : undefined}>Результаты</Text>
              </UnstyledButton>
            </Group>
          </Group>
          <Group h="100%" align="center">
            <Text mb="3">{getName()}</Text>
            <Button
              variant="transparent"
              leftSection={<LogOut size={20} />}
              onClick={async () => {
                exit();
                await navigate({ to: "/", resetScroll: true });
              }}
            />
          </Group>
        </Group>
      </AppShell.Header>
      {children}
      <AppShell.Footer p="md">
        <Grid>
          <Grid.Col span={4} offset={4}>
            <Text size="md" ta="center">
              2024
            </Text>
          </Grid.Col>
          <Grid.Col span={4}>
            <Text size="md" c="dimmed" ta="right">
              Версия клиента: {import.meta.env.VITE_WEBAPP_VERSION}
            </Text>
          </Grid.Col>
        </Grid>
      </AppShell.Footer>
    </AppShell>
  );
}

export { AppShellAdmin };
