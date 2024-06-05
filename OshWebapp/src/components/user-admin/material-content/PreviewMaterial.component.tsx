import { Api } from "../../../api/crud.ts";
import { Learning } from "../../../models/domain/Learning.tsx";
import { useEffect, useState } from "react";
import { Stack, Text, Title } from "@mantine/core";
import { PdfViewer } from "./PdfViewer.component.tsx";

interface PreviewMaterialProps {
  api: Api<Learning>;
  learningId: number;
}

function PreviewMaterial({ api, learningId }: PreviewMaterialProps) {
  const [data, setData] = useState<Learning | null>(null);

  async function fetchData() {
    const res = await api.getById(learningId);
    if (res) {
      setData(res);
    }
  }

  useEffect(() => {
    (async () => await fetchData())();
  }, [learningId]);

  return (
    <Stack>
      <Title order={1}>{data && data.name}</Title>
      <Text size="md">{data && data.text}</Text>
      {data && <PdfViewer fileUrl={`/api/s3/${data.learningSectionFile.filePath}`} />}
    </Stack>
  );
}

export { PreviewMaterial };
