import React, { Suspense } from "react";
import { createRootRoute, Outlet } from "@tanstack/react-router";

const TanStackRouterDevtools =
  process.env.NODE_ENV === "production" || process.env.NODE_ENV === "development"
    ? () => null
    : React.lazy(() =>
        import("@tanstack/router-devtools").then((res) => ({
          default: res.TanStackRouterDevtools,
        })),
      );

const TanStackQueryDevtools =
  process.env.NODE_ENV === "production" || process.env.NODE_ENV === "development"
    ? () => null
    : React.lazy(() =>
        import("@tanstack/react-query-devtools").then((res) => ({
          default: res.ReactQueryDevtools,
        })),
      );

export const Route = createRootRoute({
  component: () => (
    <>
      <Outlet />
      <Suspense>
        <TanStackQueryDevtools />
        <TanStackRouterDevtools />
      </Suspense>
    </>
  ),
});
