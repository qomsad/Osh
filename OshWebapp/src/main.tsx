import React from "react";
import ReactDOM from "react-dom/client";

import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

const queryClient = new QueryClient();

import { MantineProvider } from "@mantine/core";
import "@mantine/core/styles.css";

import { RouterProvider, createRouter } from "@tanstack/react-router";

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

import { routeTree } from "./routeTree.gen";

const router = createRouter({ routeTree });

import "@mantine/notifications/styles.css";

import { Notifications } from "@mantine/notifications";

ReactDOM.createRoot(document.querySelector("app-root")!).render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
      <MantineProvider>
        <Notifications position="top-right" />
        <RouterProvider router={router} />
      </MantineProvider>
    </QueryClientProvider>
  </React.StrictMode>,
);
