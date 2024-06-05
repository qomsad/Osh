import { AppShellEmployee } from "./AppShellEmployee.component.tsx";
import { getQuery } from "../../api/crud.ts";
import { Assigment } from "../../models/domain/Assigment.ts";
import { auth } from "../../api/api.ts";
import { Route } from "../../routes/a/program/$id.tsx";
import { useEffect, useState } from "react";
import { AssigmentLearning } from "./AssigmentLearning.component.tsx";
import { AssigmentTraining } from "./AssigmentTraining.component.tsx";
import { AppShellMain } from "@mantine/core";

function ProgramEmployee() {
  const assigmmentApi = getQuery<Assigment>("/api/employee/osh-program", auth);

  const id = parseInt(Route.useParams().id);

  const [program, setProgram] = useState<Assigment | null>();

  async function load() {
    const res = await assigmmentApi.getById(id);
    if (res) {
      setProgram(res);
    }
  }

  useEffect(() => {
    load();
  }, []);

  return (
    <AppShellEmployee>
      <AppShellMain>
        {!program?.startTraining ? <AssigmentLearning assigmentId={id} reload={load} /> : <AssigmentTraining assigmentId={id} />}
      </AppShellMain>
    </AppShellEmployee>
  );
}

export { ProgramEmployee };
