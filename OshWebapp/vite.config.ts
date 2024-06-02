import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import { env } from "process";
import { TanStackRouterVite } from '@tanstack/router-vite-plugin'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(), TanStackRouterVite()],
  server: {
    proxy: {
      "^/api": {
        target: `http://localhost:${env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(";")[0].split(":")[2] : "5000"}`,
        secure: false,
      },
    },
    port: 5173,
  },
});
