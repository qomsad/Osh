import { createFileRoute } from "@tanstack/react-router";
import { ProgramContentCreate } from "../../../components/user-admin/program-content/ProgramContentCreate.component.tsx";

export const Route = createFileRoute("/admin/program/create")({
  component: () => <ProgramContentCreate />,
});
