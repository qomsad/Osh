import { createFileRoute } from "@tanstack/react-router";
import { SpecialtyRegistry } from "../../components/user-admin/SpecialtyRegistry.component.tsx";

export const Route = createFileRoute("/admin/specialty")({
  component: () => <SpecialtyRegistry />,
});
