import { useEffect, useState } from "react";
import { auth } from "../../api/api.ts";
import { getQuery } from "../../api/crud.ts";
import { SearchPageRes } from "../../models/SearchPageRes.ts";
import { Training } from "../../models/domain/Training.ts";
import { Button, Checkbox, Stack, Title } from "@mantine/core";
import { useNavigate } from "@tanstack/react-router";

interface AssigmentTrainingProps {
  assigmentId: number;
}

function AssigmentTraining({ assigmentId }: AssigmentTrainingProps) {
  const api = getQuery<Training>(`/api/employee/osh-program/${assigmentId}/training`, auth);
  const [index, setIndex] = useState(0);
  const [trainings, setTrainings] = useState<SearchPageRes<Training>>();

  const navigate = useNavigate();

  async function load() {
    const res = await api.search("", index, 1);
    if (res) {
      setTrainings(res);
    }
  }

  useEffect(() => {
    load();
  }, [index]);

  return (
    <Stack p="xl">
      <Title order={1}>{trainings?.content && trainings.content[0]?.question}</Title>
      {trainings?.content[0]?.answers?.map((e) => {
        return (
          <>
            <Checkbox label={e.value}></Checkbox>
          </>
        );
      })}
      {index + 1 !== trainings?.totalCount ? (
        <Button onClick={() => setIndex((prev) => prev + 1)} w="200px">
          Ответить
        </Button>
      ) : (
        <Button
          onClick={async () => {
            const res = await auth().post(`/api/employee/osh-program/result/${assigmentId}`);
            if (res.data) {
              await navigate({ to: "/a" });
            }
          }}
          w="200px">
          Завершить тест
        </Button>
      )}
    </Stack>
  );
}

export { AssigmentTraining };
