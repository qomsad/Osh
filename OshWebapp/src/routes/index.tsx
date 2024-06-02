import { createFileRoute } from "@tanstack/react-router";
import { AuthWithOrganizationComponent } from "../components/auth/AuthWithOrganization.component.tsx";

export const Route = createFileRoute("/")({
  component: () => {
    return <AuthWithOrganizationComponent />;
  },
});
