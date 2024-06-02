import { createFileRoute } from "@tanstack/react-router";
import { ProgramRegistry } from "../../components/user-admin/ProgramRegistry.component.tsx";

export const Route = createFileRoute("/admin/program")({
  component: () => <ProgramRegistry />,
});
