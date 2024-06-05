import { AppShell, Button, Group, Stack, TextInput, Title } from "@mantine/core";
import { Building, Search } from "lucide-react";
import { Link } from "@tanstack/react-router";
import { AppShellOpen } from "./AppShellOpen.component.tsx";

function SetupIndexComponent() {
  return (
    <AppShellOpen>
      <AppShell.Main>
        <Stack h="100%" align="flex-start" justify="center" gap="md" p={40}>
          <Title order={1}>Организации на сервере</Title>
          <Group justify="space-between" gap={5}>
            <TextInput size="xs" w={500} placeholder="Поиск" leftSection={<Search size={14} />} />
            <Button
              variant="default"
              leftSection={<Building size={14} color="#1c8139" />}
              size="xs"
              component={Link}
              to="/setup/admin">
              <span style={{ color: "#1c8139" }}>Создать</span>
            </Button>
          </Group>
        </Stack>
      </AppShell.Main>
    </AppShellOpen>
  );
}

export { SetupIndexComponent };
