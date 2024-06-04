import { Route } from "../../../routes/admin/program/$id.tsx";

function ProgramContentView() {
  const { id } = Route.useParams();
  return <div> {id}</div>;
}

export { ProgramContentView };
