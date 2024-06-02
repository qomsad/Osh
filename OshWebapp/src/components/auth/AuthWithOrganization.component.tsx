import { AppShell, Button, Card, Grid, Group, Select, Stack, Text, TextInput, Title } from "@mantine/core";
import { Building, ShieldQuestion } from "lucide-react";
import { Link } from "@tanstack/react-router";
import { useEffect } from "react";
import { useForm } from "@mantine/form";

function AuthWithOrganizationComponent() {
  useEffect(() => {
    document.documentElement.scrollTo({
      top: 0,
      left: 0,
      behavior: "instant", // Optional if you want to skip the scrolling animation
    });
  }, []);

  const form = useForm({
    mode: "controlled",
    initialValues: {
      login: "",
      password: "",
    },
  });

  return (
    <AppShell header={{ height: 60 }} footer={{ height: 60 }} padding="md">
      <AppShell.Header>
        <Group h="100%" px="md" justify="start">
          <Group h="100%">
            <ShieldQuestion size={30} />
            <Text size="lg" fw={500} component={Link} to="/">
              АИС Охрана труда
            </Text>
          </Group>
          <Group h="100%" px="10%" align="center">
            <Text size="md">Организация:</Text>
            <Select
              leftSection={<Building size={14} />}
              data={[{ value: "usdsd", label: "ООО 'Ромашка'" }]}
              defaultValue="usdsd"
            />
          </Group>
        </Group>
      </AppShell.Header>
      <AppShell.Main>
        <Grid>
          <Grid.Col span={8}>
            <Stack h="100%" align="flex-start" justify="center" gap="md" p={40}>
              <Title order={1}>Вход в систему</Title>

              <form
                style={{ width: "100%" }}
                onSubmit={form.onSubmit(
                  (values) => {
                    console.log(values);
                  },
                  () => {
                    form.resetTouched();
                  },
                )}>
                <Card shadow="sm" padding="lg" radius="md" withBorder>
                  <TextInput
                    size="lg"
                    label="Логин"
                    w="100%"
                    labelProps={{ mb: "sm", required: true, style: { fontSize: "25px" } }}
                    key={form.key("login")}
                    {...form.getInputProps("login")}
                  />

                  <TextInput
                    size="lg"
                    label="Пароль"
                    w="100%"
                    mt="xl"
                    labelProps={{ mb: "sm", required: true, style: { fontSize: "25px" } }}
                    rightSectionWidth="10%"
                    key={form.key("password")}
                    {...form.getInputProps("password")}
                  />

                  <Button color="blue" fullWidth radius="md" mt="xl" type="submit">
                    Войти
                  </Button>
                </Card>
              </form>
            </Stack>
          </Grid.Col>
        </Grid>
      </AppShell.Main>
    </AppShell>
  );
}

export { AuthWithOrganizationComponent };
