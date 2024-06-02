import { createFileRoute } from "@tanstack/react-router";
import { AppShell } from "@mantine/core";
import { AppShellOpen } from "../../components/setup/AppShellOpen.component.tsx";
import { CreateOrganizationForm } from "../../components/setup/CreateOrganizationForm.component.tsx";

export const Route = createFileRoute("/setup/organization")({
  component: () => (
    <AppShellOpen head="Новая конфигурация">
      <AppShell.Main>
        <CreateOrganizationForm />
      </AppShell.Main>
    </AppShellOpen>
  ),
});
