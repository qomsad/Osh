import { createFileRoute } from "@tanstack/react-router";
import { AssignmentRegistry } from "../../components/user-admin/AssignmentRegistry.component.tsx";

export const Route = createFileRoute("/admin/assignment")({
  component: () => <AssignmentRegistry />,
});
