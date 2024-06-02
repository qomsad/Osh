import { createFileRoute } from "@tanstack/react-router";
import { SetupIndexComponent } from "../../components/setup/SetupIndex.component.tsx";

export const Route = createFileRoute("/setup/")({
  component: () => <SetupIndexComponent />,
});
