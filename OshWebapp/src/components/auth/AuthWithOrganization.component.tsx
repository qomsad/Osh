import {
  AppShell,
  Button,
  Card,
  Grid,
  Group,
  PasswordInput,
  Select,
  Stack,
  Text,
  TextInput,
  Title,
} from "@mantine/core";
import { Building, ShieldQuestion } from "lucide-react";
import { Link, useNavigate } from "@tanstack/react-router";
import { useEffect, useState } from "react";
import { useForm } from "@mantine/form";
import { auth, open } from "../../api/api.ts";
import { AuthRequest } from "../../models/auth/AuthRequest.ts";
import { AuthResponse } from "../../models/auth/AuthResponse.ts";
import { ErrorHandler } from "../../api/ErrorHadler.ts";
import { AuthUser } from "../../models/auth/AuthUser.ts";

function AuthWithOrganizationComponent() {
  useEffect(() => {
    document.documentElement.scrollTo({
      top: 0,
      left: 0,
      behavior: "instant",
    });
    next();
  }, []);

  const next = () => {
    auth()
      .get<AuthUser>("api/auth/current")
      .then(async (res) => {
        if (res.data) {
          if (res.data.type === "Admin") {
            await navigate({ to: "/admin", resetScroll: true });
          } else if (res.data.type === "Employee") {
            await navigate({ to: "/setup", resetScroll: true });
          }
        }
      })
      .catch(() => {});
  };

  const form = useForm({
    mode: "controlled",
    initialValues: {
      login: "",
      password: "",
    },
  });

  const [loading, setLoading] = useState(false);

  const navigate = useNavigate();

  async function authRequest(values: AuthRequest) {
    try {
      setLoading(true);
      const res = await open.post<AuthResponse>("api/auth/login", {
        login: values.login,
        password: values.password,
      });
      localStorage.setItem("auth", JSON.stringify(res.data));
      next();
    } catch (error) {
      ErrorHandler(error);
    } finally {
      setLoading(false);
    }
  }

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
              data={[{ value: window.location.hostname, label: "По умолчанию" }]}
              defaultValue={window.location.hostname}
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
                onSubmit={form.onSubmit(authRequest, () => {
                  form.resetTouched();
                })}>
                <Card shadow="sm" padding="lg" radius="md" withBorder>
                  <TextInput
                    size="lg"
                    label="Логин"
                    w="100%"
                    labelProps={{ mb: "sm", required: true, style: { fontSize: "25px" } }}
                    key={form.key("login")}
                    {...form.getInputProps("login")}
                  />

                  <PasswordInput
                    size="lg"
                    label="Пароль"
                    w="100%"
                    mt="xl"
                    labelProps={{ mb: "sm", required: true, style: { fontSize: "25px" } }}
                    rightSectionWidth="10%"
                    key={form.key("password")}
                    {...form.getInputProps("password")}
                  />
                  <Button color="blue" fullWidth radius="md" mt="xl" type="submit" loading={loading}>
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
