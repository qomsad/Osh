import React, { useEffect, useRef, useState } from "react";
import { Document, Page, pdfjs } from "react-pdf";
import axios from "axios";
import { ActionIcon, Card, Container, Grid, Group, Loader, Stack, Text } from "@mantine/core";
import { ZoomIn, ZoomOut } from "lucide-react";

pdfjs.GlobalWorkerOptions.workerSrc = new URL("pdfjs-dist/build/pdf.worker.min.mjs", import.meta.url).toString();

interface PdfViewerProps {
  fileUrl: string;
}

const PdfViewer: React.FC<PdfViewerProps> = ({ fileUrl }) => {
  const [numPages, setNumPages] = useState<number | null>(null);
  const [pdfData, setPdfData] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const viewerRef = useRef<HTMLDivElement>(null);
  const [scale, setScale] = useState(1.0);

  const increaseScale = () => {
    setScale(scale + 0.1);
  };

  const decreaseScale = () => {
    setScale(scale - 0.1);
  };

  useEffect(() => {
    const fetchPdf = async () => {
      try {
        const response = await axios.get(fileUrl, {
          responseType: "arraybuffer",
        });
        const blob = new Blob([response.data], { type: "application/pdf" });
        const url = URL.createObjectURL(blob);
        setPdfData(url);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching PDF:", error);
        setLoading(false);
      }
    };

    fetchPdf();
  }, [fileUrl]);

  const onDocumentLoadSuccess = ({ numPages }: { numPages: number }) => {
    setNumPages(numPages);
  };

  const onPageChange = () => {
    if (viewerRef.current) {
      const pages = viewerRef.current.querySelectorAll(".react-pdf__Page");
      pages.forEach((page, index) => {
        const { top, bottom } = page.getBoundingClientRect();
        if (top < window.innerHeight && bottom > 0) {
          setCurrentPage(index + 1);
        }
      });
    }
  };

  if (loading) {
    return (
      <Container>
        <Loader />
      </Container>
    );
  }

  return (
    <Stack w="100%">
      {pdfData ? (
        <>
          <Grid>
            <Grid.Col span={2}>
              <Group w="400px">
                <ActionIcon onClick={decreaseScale} disabled={scale <= 0.5} color="dark">
                  <ZoomOut />
                </ActionIcon>
                <Text>Масштаб: {Math.round(scale * 100)}%</Text>
                <ActionIcon onClick={increaseScale} disabled={scale >= 2.0} color="dark">
                  <ZoomIn />
                </ActionIcon>
              </Group>
            </Grid.Col>
            <Grid.Col span={4} offset={4}>
              <Text fw={700} w="300">
                Страница {currentPage} из {numPages}
              </Text>
            </Grid.Col>
          </Grid>

          <Card shadow="sm" radius="md" withBorder>
            <div ref={viewerRef} onScroll={onPageChange} style={{ height: "63vh", overflowY: "scroll", width: "100%" }}>
              <Document file={pdfData} onLoadSuccess={onDocumentLoadSuccess}>
                {Array.from(new Array(numPages), (_, index) => (
                  <Page key={`page_${index + 1}`} pageNumber={index + 1} scale={scale} />
                ))}
              </Document>
            </div>
          </Card>
        </>
      ) : (
        <Text>Ошибка загрузки файла</Text>
      )}
    </Stack>
  );
};

export { PdfViewer };
