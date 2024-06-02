import { Badge, Button, Card, Group, Text } from "@mantine/core";
import { Building, KeyRound } from "lucide-react";

interface CardOrganizationOpenComponentProps {
  name: string;
  url: string;
}

function CardOrganizationOpenComponent({ name, url }: CardOrganizationOpenComponentProps) {
  return (
    <Card shadow="sm" padding="lg" radius="md" withBorder w="50%">
      <Group justify="space-between" align="start" mb="xs">
        <Group justify="flex-start" align="end">
          <Building size={50} />
          <Text size="lg" fw={500}>
            {name}
          </Text>
        </Group>
        <Badge color="black">{url}</Badge>
      </Group>
      <Button variant="default" leftSection={<KeyRound size={14} color="#0969dd" />} size="xs">
        <span style={{ color: "#0969dd" }}>Вход</span>
      </Button>
    </Card>
  );
}

export { CardOrganizationOpenComponent };
