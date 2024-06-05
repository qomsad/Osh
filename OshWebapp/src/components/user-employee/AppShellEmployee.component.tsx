import { Link, useNavigate } from "@tanstack/react-router";
import { AppShell, Button, Grid, Group, Text, UnstyledButton } from "@mantine/core";
import { LogOut, ShieldQuestion } from "lucide-react";
import { exit, getName } from "../../api/api.ts";
import React from "react";

interface AppShellEmployeeProps {
  children?: React.ReactNode;
}

function AppShellEmployee({ children }: AppShellEmployeeProps) {
  const navigate = useNavigate();

  return (
    <AppShell header={{ height: 60 }} footer={{ height: 60 }} padding="md">
      <AppShell.Header>
        <Group h="100%" px="md" justify="space-between">
          <Group h="100%">
            <ShieldQuestion size={30} />
            <Text size="lg" fw={500} component={Link} to="/a">
              АИС Охрана труда
            </Text>
            <Group h="100%" px="md" align="center">
              <UnstyledButton component={Link} to="/a">
                <Text>Программы обучения</Text>
              </UnstyledButton>
              <UnstyledButton component={Link} to="/a/results">
                <Text>Результаты</Text>
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

export { AppShellEmployee };
