import { useEffect, useState } from "react";
import { Assigment } from "../../models/domain/Assigment.ts";
import { auth } from "../../api/api.ts";
import { getQuery } from "../../api/crud.ts";

interface AssigmentTrainingProps {
  assigmentId: number;
}

function AssigmentTraining({ assigmentId }: AssigmentTrainingProps) {
  const assigmmentApi = getQuery<Assigment>("/api/employee/osh-program", auth);
  const [program, setProgram] = useState<Assigment | null>();

  async function load() {
    const res = await assigmmentApi.getById(assigmentId);
    if (res) {
      setProgram(res);
      if (res.startTraining === null) {
        await auth().patch(`/api/employee/osh-program/${assigmentId}/start-learning`);
      }
    }
  }

  useEffect(() => {
    load();
  }, []);

  return <>Training{assigmentId}</>;
}

export { AssigmentTraining };
