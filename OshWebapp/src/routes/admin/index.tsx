import { createFileRoute } from "@tanstack/react-router";
import { AppShellAdmin } from "../../components/user-admin/AppShellAdmin.component.tsx";

export const Route = createFileRoute("/admin/")({
  component: () => <AppShellAdmin />,
});
