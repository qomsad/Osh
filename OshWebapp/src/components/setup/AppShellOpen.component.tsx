import { AppShell, Grid, Group, Text, TextInput } from "@mantine/core";
import { Database, ShieldQuestion } from "lucide-react";
import React from "react";
import { Link } from "@tanstack/react-router";

interface AppShellOpenProps {
  children?: React.ReactNode;
  head?: string;
}

function AppShellOpen({ children, head }: AppShellOpenProps) {
  return (
    <AppShell header={{ height: 60 }} footer={{ height: 60 }} padding="md">
      <AppShell.Header>
        <Group h="100%" px="md" justify="space-between">
          <Group h="100%">
            <ShieldQuestion size={30} />
            <Text size="lg" fw={500} component={Link} to="/">
              АИС Охрана труда
            </Text>
            {head && (
              <Text size="lg" fw="bolder">
                {head}
              </Text>
            )}
          </Group>
          <Group h="100%" px="md" align="center">
            <Text size="md">Сервер:</Text>
            <TextInput
              leftSection={<Database size={14} />}
              disabled
              value={`${window.location.protocol}//${window.location.hostname}:${window.location.port}`}
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

export { AppShellOpen };
