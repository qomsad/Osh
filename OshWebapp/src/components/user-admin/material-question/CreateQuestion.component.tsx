import { Api } from "../../../api/crud.ts";
import { Training } from "../../../models/domain/Training.ts";

interface CreateQuestionProps {}

interface CreateQuestionProps {
  onOk?: () => Promise<void>;
  onCancel?: () => void;
  api?: Api<Training>;
}

// @ts-ignore
// eslint-disable-next-line @typescript-eslint/no-unused-vars
function CreateQuestion({ onOk, onCancel, api }: CreateQuestionProps) {
  return <>CreateQuestion</>;
}

export { CreateQuestion };
