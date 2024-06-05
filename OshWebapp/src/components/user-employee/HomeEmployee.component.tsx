import { AppShellEmployee } from "./AppShellEmployee.component.tsx";
import { AppShell, Card, Group, SimpleGrid, Stack, Text, Title, UnstyledButton } from "@mantine/core";
import { useNavigate } from "@tanstack/react-router";
import { getQuery } from "../../api/crud.ts";
import { auth } from "../../api/api.ts";
import { useEffect, useState } from "react";
import { Assigment } from "../../models/domain/Assigment.ts";

function HomeEmployee() {
  const programsApi = getQuery<Assigment>("/api/employee/osh-program", auth);

  const [programs, setPrograms] = useState<Assigment[]>([]);

  async function load() {
    const res = await programsApi.search("", 0, 20);
    if (res.content) {
      setPrograms(res.content);
    }
  }

  useEffect(() => {
    load();
  }, []);

  return (
    <AppShellEmployee>
      <AppShell.Main>
        <Stack h="100%" align="flex-start" justify="center" gap="md" p={40}>
          <Title order={1}>Вам нужно пройти обучение по данным темам:</Title>
          <SimpleGrid cols={3}>
            {programs.map((program) => (
              <Program
                key={program.id}
                specialty={program.oshProgram.specialty.name}
                name={program.oshProgram.name}
                date={program.assignmentDate}
                url={`/a/program/${program.id}`}
                id={program.id}
                need={!program.startTraining}
              />
            ))}
          </SimpleGrid>
        </Stack>
      </AppShell.Main>
    </AppShellEmployee>
  );
}

function Program({
  url,
  name,
  specialty,
  date,
  id,
  need,
}: {
  url: string;
  name: string;
  specialty: string;
  date: string;
  id: number;
  need?: boolean;
}) {
  const navigate = useNavigate();
  return (
    <UnstyledButton
      onClick={async () => {
        if (need) {
          await auth().patch(`/api/employee/osh-program/${id}/start-learning`);
        }
        navigate({ to: url });
      }}>
      <Card shadow="sm" padding="lg" radius="md" withBorder>
        <Text size="xs" ta="center">
          {specialty}
        </Text>
        <Group justify="start" align="center" mb="xs">
          <Text size="lg" fw={500}>
            {name}
          </Text>
        </Group>
        <Stack>
          <Text size="xs">Назначена: {new Date(date).toLocaleString()}</Text>
        </Stack>
      </Card>
    </UnstyledButton>
  );
}

export { HomeEmployee };
