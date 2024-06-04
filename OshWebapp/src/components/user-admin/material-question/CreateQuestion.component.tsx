import { Api } from "../../../api/crud.ts";
import { Training } from "../../../models/domain/Training.ts";
import { useState } from "react";
import { ActionIcon, Button, Center, Checkbox, Group, Stack, Text, Textarea, TextInput } from "@mantine/core";
import { DragDropContext, Draggable, Droppable } from "@hello-pangea/dnd";
import { GripVertical, Trash2 } from "lucide-react";
import { isNotEmpty, useForm } from "@mantine/form";
import { randomId } from "@mantine/hooks";

interface CreateQuestionProps {}

interface CreateQuestionProps {
  onOk: () => Promise<void>;
  onCancel?: () => void;
  api: Api<Training>;
}

function CreateQuestion({ onOk, onCancel, api }: CreateQuestionProps) {
  const [loading, setLoading] = useState(false);

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      question: "",
      answers: [{ value: "", isRight: false, key: randomId() }],
    },
    validate: {
      question: isNotEmpty("Вопрос должен быть заполен"),
    },
  });

  const fields = form.getValues().answers.map((item, index) => (
    <Draggable key={item.key} index={index} draggableId={item.key}>
      {(provided) => (
        <Group ref={provided.innerRef} mt="xs" {...provided.draggableProps}>
          <Center {...provided.dragHandleProps}>
            <GripVertical />
          </Center>
          <Checkbox key={form.key(`answers.${index}.isRight`)} {...form.getInputProps(`answers.${index}.isRight`)} />
          <TextInput
            w="79%"
            variant="unstyled"
            placeholder="Ответ на вопрос"
            key={form.key(`answers.${index}.value`)}
            {...form.getInputProps(`answers.${index}.value`)}
          />
          <ActionIcon variant="transparent" color="red" onClick={() => form.removeListItem("answers", index)}>
            <Trash2 size="15px" />
          </ActionIcon>
        </Group>
      )}
    </Draggable>
  ));

  return (
    <form
      onSubmit={form.onSubmit(async (values) => {
        setLoading(true);
        if (values.answers.length > 0) {
          const val = {
            ...values,
            answers: values.answers.map((item, index) => ({ ...item, index: index + 1 })),
            questionType: "QuestionMultiple",
            rate: 1,
          };
          const er = await api?.create(val);
          if (er != "ERROR") {
            await onOk();
          }
        }
        setLoading(false);
      })}>
      <Stack>
        <Textarea
          placeholder="Текст вопроса"
          label="Вопрос"
          autosize
          key={form.key("question")}
          {...form.getInputProps("question")}
        />
        <div>
          <Group
            justify="start"
            onClick={() => form.insertListItem("answers", { value: "", isRight: false, key: randomId() })}>
            <Text size="sm" fw={500}>
              Ответы
            </Text>
            <Button size="compact-xs" variant="outline" color="dark">
              Добавить ответ
            </Button>
          </Group>
          <DragDropContext
            onDragEnd={({ destination, source }) =>
              destination?.index !== undefined &&
              form.reorderListItem("answers", {
                from: source.index,
                to: destination.index,
              })
            }>
            <Droppable droppableId="dnd-list" direction="vertical">
              {(provided) => (
                <div {...provided.droppableProps} ref={provided.innerRef}>
                  {fields}
                  {provided.placeholder}
                </div>
              )}
            </Droppable>
          </DragDropContext>
        </div>
      </Stack>
      <Group>
        <Button variant="filled" color="green" radius="md" mt="md" type="submit" loading={loading}>
          Создать
        </Button>
        <Button variant="light" radius="md" mt="md" onClick={onCancel}>
          Отмена
        </Button>
      </Group>
    </form>
  );
}

export { CreateQuestion };
