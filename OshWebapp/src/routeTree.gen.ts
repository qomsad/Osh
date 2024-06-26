/* prettier-ignore-start */

/* eslint-disable */

// @ts-nocheck

// noinspection JSUnusedGlobalSymbols

// This file is auto-generated by TanStack Router

// Import Routes

import { Route as rootRoute } from './routes/__root'
import { Route as IndexImport } from './routes/index'
import { Route as SetupIndexImport } from './routes/setup/index'
import { Route as AdminIndexImport } from './routes/admin/index'
import { Route as AIndexImport } from './routes/a/index'
import { Route as SetupOrganizationImport } from './routes/setup/organization'
import { Route as SetupAdminImport } from './routes/setup/admin'
import { Route as AdminSpecialtyImport } from './routes/admin/specialty'
import { Route as AdminResultImport } from './routes/admin/result'
import { Route as AdminEmployeeImport } from './routes/admin/employee'
import { Route as AdminAssignmentImport } from './routes/admin/assignment'
import { Route as AdminProgramIndexImport } from './routes/admin/program/index'
import { Route as AdminProgramCreateImport } from './routes/admin/program/create'
import { Route as AdminProgramIdImport } from './routes/admin/program/$id'
import { Route as AProgramIdImport } from './routes/a/program/$id'

// Create/Update Routes

const IndexRoute = IndexImport.update({
  path: '/',
  getParentRoute: () => rootRoute,
} as any)

const SetupIndexRoute = SetupIndexImport.update({
  path: '/setup/',
  getParentRoute: () => rootRoute,
} as any)

const AdminIndexRoute = AdminIndexImport.update({
  path: '/admin/',
  getParentRoute: () => rootRoute,
} as any)

const AIndexRoute = AIndexImport.update({
  path: '/a/',
  getParentRoute: () => rootRoute,
} as any)

const SetupOrganizationRoute = SetupOrganizationImport.update({
  path: '/setup/organization',
  getParentRoute: () => rootRoute,
} as any)

const SetupAdminRoute = SetupAdminImport.update({
  path: '/setup/admin',
  getParentRoute: () => rootRoute,
} as any)

const AdminSpecialtyRoute = AdminSpecialtyImport.update({
  path: '/admin/specialty',
  getParentRoute: () => rootRoute,
} as any)

const AdminResultRoute = AdminResultImport.update({
  path: '/admin/result',
  getParentRoute: () => rootRoute,
} as any)

const AdminEmployeeRoute = AdminEmployeeImport.update({
  path: '/admin/employee',
  getParentRoute: () => rootRoute,
} as any)

const AdminAssignmentRoute = AdminAssignmentImport.update({
  path: '/admin/assignment',
  getParentRoute: () => rootRoute,
} as any)

const AdminProgramIndexRoute = AdminProgramIndexImport.update({
  path: '/admin/program/',
  getParentRoute: () => rootRoute,
} as any)

const AdminProgramCreateRoute = AdminProgramCreateImport.update({
  path: '/admin/program/create',
  getParentRoute: () => rootRoute,
} as any)

const AdminProgramIdRoute = AdminProgramIdImport.update({
  path: '/admin/program/$id',
  getParentRoute: () => rootRoute,
} as any)

const AProgramIdRoute = AProgramIdImport.update({
  path: '/a/program/$id',
  getParentRoute: () => rootRoute,
} as any)

// Populate the FileRoutesByPath interface

declare module '@tanstack/react-router' {
  interface FileRoutesByPath {
    '/': {
      id: '/'
      path: '/'
      fullPath: '/'
      preLoaderRoute: typeof IndexImport
      parentRoute: typeof rootRoute
    }
    '/admin/assignment': {
      id: '/admin/assignment'
      path: '/admin/assignment'
      fullPath: '/admin/assignment'
      preLoaderRoute: typeof AdminAssignmentImport
      parentRoute: typeof rootRoute
    }
    '/admin/employee': {
      id: '/admin/employee'
      path: '/admin/employee'
      fullPath: '/admin/employee'
      preLoaderRoute: typeof AdminEmployeeImport
      parentRoute: typeof rootRoute
    }
    '/admin/result': {
      id: '/admin/result'
      path: '/admin/result'
      fullPath: '/admin/result'
      preLoaderRoute: typeof AdminResultImport
      parentRoute: typeof rootRoute
    }
    '/admin/specialty': {
      id: '/admin/specialty'
      path: '/admin/specialty'
      fullPath: '/admin/specialty'
      preLoaderRoute: typeof AdminSpecialtyImport
      parentRoute: typeof rootRoute
    }
    '/setup/admin': {
      id: '/setup/admin'
      path: '/setup/admin'
      fullPath: '/setup/admin'
      preLoaderRoute: typeof SetupAdminImport
      parentRoute: typeof rootRoute
    }
    '/setup/organization': {
      id: '/setup/organization'
      path: '/setup/organization'
      fullPath: '/setup/organization'
      preLoaderRoute: typeof SetupOrganizationImport
      parentRoute: typeof rootRoute
    }
    '/a/': {
      id: '/a/'
      path: '/a'
      fullPath: '/a'
      preLoaderRoute: typeof AIndexImport
      parentRoute: typeof rootRoute
    }
    '/admin/': {
      id: '/admin/'
      path: '/admin'
      fullPath: '/admin'
      preLoaderRoute: typeof AdminIndexImport
      parentRoute: typeof rootRoute
    }
    '/setup/': {
      id: '/setup/'
      path: '/setup'
      fullPath: '/setup'
      preLoaderRoute: typeof SetupIndexImport
      parentRoute: typeof rootRoute
    }
    '/a/program/$id': {
      id: '/a/program/$id'
      path: '/a/program/$id'
      fullPath: '/a/program/$id'
      preLoaderRoute: typeof AProgramIdImport
      parentRoute: typeof rootRoute
    }
    '/admin/program/$id': {
      id: '/admin/program/$id'
      path: '/admin/program/$id'
      fullPath: '/admin/program/$id'
      preLoaderRoute: typeof AdminProgramIdImport
      parentRoute: typeof rootRoute
    }
    '/admin/program/create': {
      id: '/admin/program/create'
      path: '/admin/program/create'
      fullPath: '/admin/program/create'
      preLoaderRoute: typeof AdminProgramCreateImport
      parentRoute: typeof rootRoute
    }
    '/admin/program/': {
      id: '/admin/program/'
      path: '/admin/program'
      fullPath: '/admin/program'
      preLoaderRoute: typeof AdminProgramIndexImport
      parentRoute: typeof rootRoute
    }
  }
}

// Create and export the route tree

export const routeTree = rootRoute.addChildren({
  IndexRoute,
  AdminAssignmentRoute,
  AdminEmployeeRoute,
  AdminResultRoute,
  AdminSpecialtyRoute,
  SetupAdminRoute,
  SetupOrganizationRoute,
  AIndexRoute,
  AdminIndexRoute,
  SetupIndexRoute,
  AProgramIdRoute,
  AdminProgramIdRoute,
  AdminProgramCreateRoute,
  AdminProgramIndexRoute,
})

/* prettier-ignore-end */

/* ROUTE_MANIFEST_START
{
  "routes": {
    "__root__": {
      "filePath": "__root.tsx",
      "children": [
        "/",
        "/admin/assignment",
        "/admin/employee",
        "/admin/result",
        "/admin/specialty",
        "/setup/admin",
        "/setup/organization",
        "/a/",
        "/admin/",
        "/setup/",
        "/a/program/$id",
        "/admin/program/$id",
        "/admin/program/create",
        "/admin/program/"
      ]
    },
    "/": {
      "filePath": "index.tsx"
    },
    "/admin/assignment": {
      "filePath": "admin/assignment.tsx"
    },
    "/admin/employee": {
      "filePath": "admin/employee.tsx"
    },
    "/admin/result": {
      "filePath": "admin/result.tsx"
    },
    "/admin/specialty": {
      "filePath": "admin/specialty.tsx"
    },
    "/setup/admin": {
      "filePath": "setup/admin.tsx"
    },
    "/setup/organization": {
      "filePath": "setup/organization.tsx"
    },
    "/a/": {
      "filePath": "a/index.tsx"
    },
    "/admin/": {
      "filePath": "admin/index.tsx"
    },
    "/setup/": {
      "filePath": "setup/index.tsx"
    },
    "/a/program/$id": {
      "filePath": "a/program/$id.tsx"
    },
    "/admin/program/$id": {
      "filePath": "admin/program/$id.tsx"
    },
    "/admin/program/create": {
      "filePath": "admin/program/create.tsx"
    },
    "/admin/program/": {
      "filePath": "admin/program/index.tsx"
    }
  }
}
ROUTE_MANIFEST_END */
