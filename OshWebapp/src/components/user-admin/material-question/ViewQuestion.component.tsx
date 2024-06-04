import { Api } from "../../../api/crud.ts";
import { Training } from "../../../models/domain/Training.ts";

interface ViewQuestionProps {}

interface ViewQuestionProps {
  id?: number;
  api?: Api<Training>;
}

// @ts-ignore
// eslint-disable-next-line @typescript-eslint/no-unused-vars
function ViewQuestion({ id, api }: ViewQuestionProps) {
  return <>ViewQuestion</>;
}

export { ViewQuestion };
