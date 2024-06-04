import { useEffect, useState } from "react";
import { isEmail, matches, matchesField, useForm } from "@mantine/form";
import { Button, Card, Grid, PasswordInput, Stack, Stepper, Text, TextInput, Title } from "@mantine/core";
import { useNavigate } from "@tanstack/react-router";

function CreateSuperUserFrom() {
  const form = useForm({
    mode: "controlled",
    initialValues: {
      login: "",
      password: "",
      passwordC: "",
      email: "",
      lastName: "",
      firstName: "",
      secondName: "",
    },
    validate: {
      login: matches(/^[a-z0-9_]{3,}$/, "Логин должен содержать только английские символы, цифры и '_'"),
      password: matches(/^[!-~]{6,}$/, "Пароль должен быть 6 символов"),
      passwordC: matchesField("password", "Пароли должны совпадать"),
      email: isEmail("Невалидный адрес"),
    },
  });
  const [isButtonDisabled, setIsButtonDisabled] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    setIsButtonDisabled(!form.isTouched());
  }, [form, form.isTouched]);
  return (
    <Grid>
      <Grid.Col span={8}>
        <Stack h="100%" align="flex-start" justify="center" gap="md" p={40}>
          <Title order={1}>Создание главного администратора</Title>

          <form
            style={{ width: "100%" }}
            onSubmit={form.onSubmit(
              (values) => {
                // todo
                console.log(values);
              },
              () => {
                form.resetTouched();
                setIsButtonDisabled(true);
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
              <Text size="sm" mt="xs" c="dimmed">
                Используется для авторизации.
              </Text>

              <PasswordInput
                size="lg"
                label="Пароль"
                w="100%"
                mt="lg"
                labelProps={{ mb: "sm", required: true, style: { fontSize: "25px" } }}
                key={form.key("password")}
                {...form.getInputProps("password")}
              />
              <Text size="sm" mt="xs" c="dimmed">
                Минимум 6 символов.
              </Text>

              <PasswordInput
                size="lg"
                label="Подверждение пароля"
                w="100%"
                mt="lg"
                labelProps={{ mb: "sm", required: true, style: { fontSize: "25px" } }}
                key={form.key("passwordC")}
                {...form.getInputProps("passwordC")}
              />
              <Text size="sm" mt="xs" c="dimmed">
                Пожалуйста запомните свой пароль.
              </Text>

              <TextInput
                size="lg"
                label="Электорнная почта"
                w="100%"
                mt="lg"
                labelProps={{ mb: "sm", required: true, style: { fontSize: "25px" } }}
                key={form.key("email")}
                {...form.getInputProps("email")}
              />
              <Text size="sm" mt="xs" c="dimmed">
                Используется для уведомлений и восстановления доступа.
              </Text>

              <TextInput
                size="lg"
                label="Фамимлия"
                w="100%"
                mt="lg"
                labelProps={{ mb: "sm", required: false, style: { fontSize: "25px" } }}
                key={form.key("lastName")}
                {...form.getInputProps("lastName")}
              />

              <TextInput
                size="lg"
                label="Имя"
                w="100%"
                mt="lg"
                labelProps={{ mb: "sm", required: false, style: { fontSize: "25px" } }}
                key={form.key("firstName")}
                {...form.getInputProps("firstName")}
              />

              <TextInput
                size="lg"
                label="Отчество"
                w="100%"
                mt="lg"
                labelProps={{ mb: "sm", required: false, style: { fontSize: "25px" } }}
                key={form.key("secondName")}
                {...form.getInputProps("secondName")}
              />

              <Button color="blue" fullWidth radius="md" mt="xl" type="submit" disabled={isButtonDisabled}>
                {isButtonDisabled ? (
                  <span style={{ color: "red" }}>Проверьте правильность заполнения полей</span>
                ) : (
                  <span>Создать супер пользователя</span>
                )}
              </Button>
              {/* todo*/}
              <Button
                variant="default"
                size="xs"
                onClick={() => navigate({ to: "/setup/organization", resetScroll: true })}>
                <span style={{ color: "#1c8139" }}>Создать</span>
              </Button>
            </Card>
          </form>
        </Stack>
      </Grid.Col>
      <Grid.Col span={4} mt={70}>
        <Stepper active={0} orientation="vertical" p={40}>
          <Stepper.Step label="Шаг 1" description="Создание главного администратора" />
          <Stepper.Step label="Шаг 2" description="Создание ораганиазции" />
          <Stepper.Step label="Шаг 3" description="Инициализация системы" />
        </Stepper>
      </Grid.Col>
    </Grid>
  );
}

export { CreateSuperUserFrom };
