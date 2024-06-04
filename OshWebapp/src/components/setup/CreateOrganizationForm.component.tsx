import { Button, Card, Checkbox, Grid, Select, Stack, Stepper, Text, TextInput, Title } from "@mantine/core";
import { isNotEmpty, matches, useForm } from "@mantine/form";
import { useEffect, useState } from "react";
import { useNavigate } from "@tanstack/react-router";

function CreateOrganizationForm() {
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
      name: "",
      url: "localhost",
      superUser: "user",
    },
    validate: {
      name: isNotEmpty("Название не может быть пустым"),
      url: matches(/^[a-z_][a-z0-9_]*(\.[a-z_][a-z0-9_]*)*$/, "Невалидный URL"),
    },
  });
  const [isUrlCheck, setIsUrlCheck] = useState(true);
  const [isButtonDisabled, setIsButtonDisabled] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    setIsButtonDisabled(!form.isTouched());
  }, [form, form.isTouched]);
  return (
    <Grid>
      <Grid.Col span={8}>
        <Stack h="100%" align="flex-start" justify="center" gap="md" p={40}>
          <Title order={1}>Создание организации</Title>

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
                label="Название"
                w="100%"
                labelProps={{ mb: "sm", required: true, style: { fontSize: "25px" } }}
                key={form.key("name")}
                {...form.getInputProps("name")}
              />
              <Text size="sm" mt="xs" c="dimmed">
                Название организации, отображается при входе в учетную запись.
              </Text>

              <TextInput
                size="lg"
                label="URL"
                w="100%"
                mt="xl"
                labelProps={{ mb: "sm", required: true, style: { fontSize: "25px" } }}
                rightSectionWidth="10%"
                key={form.key("url")}
                disabled={isUrlCheck}
                {...form.getInputProps("url")}
              />
              <Checkbox
                checked={isUrlCheck}
                mt="xs"
                onChange={() => {
                  setIsUrlCheck(!isUrlCheck);
                  if (!isUrlCheck) {
                    form.setFieldValue("url", window.location.hostname);
                  } else {
                    form.setFieldValue("url", "");
                  }
                }}
                label="Ипользовать текуший"
              />

              <Text size="sm" mt="xs" c="dimmed">
                Уникальный интернет адрес, на котором расползается система.
                <br />
                Система будет располагаться:
                <span style={{ fontFamily: "monospace" }}>
                  {" " + window.location.protocol + "//"}
                  {form.values.url !== window.location.hostname && form.values.url !== "" ? (
                    <span>
                      {form.values.url}.{window.location.hostname}
                    </span>
                  ) : (
                    window.location.hostname
                  )}
                  :{window.location.port}
                </span>
              </Text>

              <Select
                label="Главный администратор"
                data={[{ value: "user", label: "Иванов Иван Иванович (user)" }]}
                size="lg"
                w="100%"
                mt="xl"
                labelProps={{ mb: "sm", required: true, style: { fontSize: "25px" } }}
                key={form.key("superUser")}
                {...form.getInputProps("superUser")}
              />
              <Button color="blue" fullWidth radius="md" mt="xl" type="submit" disabled={isButtonDisabled}>
                {isButtonDisabled ? (
                  <span style={{ color: "red" }}>Проверьте правильность заполнения полей</span>
                ) : (
                  <span>Создать организацию</span>
                )}
              </Button>
              <Button variant="default" size="xs" onClick={() => navigate({ to: "/", resetScroll: true })}>
                <span style={{ color: "#1c8139" }}>Создать</span>
              </Button>
            </Card>
          </form>
        </Stack>
      </Grid.Col>
      <Grid.Col span={4} mt={70}>
        <Stepper active={1} orientation="vertical" p={40}>
          <Stepper.Step label="Шаг 1" description="Создание главного администратора" />
          <Stepper.Step label="Шаг 2" description="Создание ораганиазции" />
          <Stepper.Step label="Шаг 3" description="Инициализация системы" />
        </Stepper>
      </Grid.Col>
    </Grid>
  );
}

export { CreateOrganizationForm };
