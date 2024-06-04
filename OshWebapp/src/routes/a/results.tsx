import { createFileRoute } from "@tanstack/react-router";
import { ResultsEmployee } from "../../components/user-employee/ResultsEmployee.component.tsx";

export const Route = createFileRoute("/a/results")({
  component: () => <ResultsEmployee />,
});
