import { createFileRoute } from "@tanstack/react-router";
import { EmployeeRegistry } from "../../components/user-admin/EmployeeRegistry.component.tsx";

export const Route = createFileRoute("/admin/employee")({
  component: () => <EmployeeRegistry />,
});
