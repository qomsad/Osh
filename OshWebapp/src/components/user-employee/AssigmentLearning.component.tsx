import { Button, Card, Grid, Stack, Title, UnstyledButton } from "@mantine/core";
import { getQuery } from "../../api/crud.ts";
import { Learning } from "../../models/domain/Learning.tsx";
import { auth } from "../../api/api.ts";
import { useEffect, useState } from "react";
import { SearchPageRes } from "../../models/SearchPageRes.ts";
import { PreviewMaterial } from "../user-admin/material-content/PreviewMaterial.component.tsx";

interface AssigmentLearningProps {
  assigmentId: number;
}

function AssigmentLearning({ assigmentId }: AssigmentLearningProps) {
  const api = getQuery<Learning>(`/api/employee/osh-program/${assigmentId}/learning`, auth);

  const [learnings, setLearnings] = useState<SearchPageRes<Learning>>();
  const [index, setIndex] = useState(0);

  async function load() {
    const res = await api.search("", index, 1);
    if (res) {
      setLearnings(res);
    }
  }

  useEffect(() => {
    load();
  }, [index]);

  const lerans: number[] = [];
  if (learnings) {
    for (let i = 0; i < learnings.totalCount; i++) {
      lerans.push(i);
    }
  }

  return (
    <Grid h="60vh">
      <Grid.Col span={2}>
        <Stack h="80vh" justify="space-between" gap="lg">
          <Title order={1}>Обучение</Title>
          <Stack h="100%">
            <Card shadow="sm" px="lg" radius="md" withBorder style={{ height: "100%" }}>
              {learnings != null &&
                lerans.map((e) => {
                  return (
                    <UnstyledButton
                      key={e}
                      style={{ fontSize: "15pt" }}
                      fw={index === e ? 700 : undefined}
                      onClick={() => setIndex(e)}>
                      Материал {e + 1} {}
                    </UnstyledButton>
                  );
                })}
            </Card>
            <Button>Приступить к тесту</Button>
          </Stack>
        </Stack>
      </Grid.Col>
      <Grid.Col span={10} style={{ scale: "0.9", transformOrigin: "top left" }}>
        <PreviewMaterial api={api} learningId={learnings?.content[0].id ?? 0} />
      </Grid.Col>
    </Grid>
  );
}

export { AssigmentLearning };
