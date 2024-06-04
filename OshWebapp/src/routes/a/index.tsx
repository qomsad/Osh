import { createFileRoute } from "@tanstack/react-router";
import { HomeEmployee } from "../../components/user-employee/HomeEmployee.component.tsx";

export const Route = createFileRoute("/a/")({
  component: () => <HomeEmployee/>
});
