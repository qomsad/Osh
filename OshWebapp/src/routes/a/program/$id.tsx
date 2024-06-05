import { createFileRoute } from "@tanstack/react-router";
import { ProgramEmployee } from "../../../components/user-employee/ProgramEmployee.component.tsx";

export const Route = createFileRoute("/a/program/$id")({
  component: () => <ProgramEmployee />,
});
