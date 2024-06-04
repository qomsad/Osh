import { createFileRoute } from "@tanstack/react-router";
import { ProgramContentView } from "../../../components/user-admin/program-content/ProgramContentView.component.tsx";

export const Route = createFileRoute("/admin/program/$id")({
  component: () => <ProgramContentView />,
});
