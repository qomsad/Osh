import { createFileRoute } from "@tanstack/react-router";
import { AppShell } from "@mantine/core";
import { CreateSuperUserFrom } from "../../components/setup/CreateSuperUserFrom.component.tsx";
import { AppShellOpen } from "../../components/setup/AppShellOpen.component.tsx";

export const Route = createFileRoute("/setup/admin")({
  component: () => (
    <AppShellOpen head="Новая конфигурация">
      <AppShell.Main>
        <CreateSuperUserFrom />
      </AppShell.Main>
    </AppShellOpen>
  ),
});
