import { createFileRoute } from "@tanstack/react-router";
import { ResultRegistry } from "../../components/user-admin/ResultRegistry.component.tsx";

export const Route = createFileRoute("/admin/result")({
  component: () => <ResultRegistry />,
});
