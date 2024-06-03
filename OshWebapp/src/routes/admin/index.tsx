import { createFileRoute } from "@tanstack/react-router";
import { AdminHome } from "../../components/user-admin/AdminHome.component.tsx";

export const Route = createFileRoute("/admin/")({
  component: () => <AdminHome />,
});
