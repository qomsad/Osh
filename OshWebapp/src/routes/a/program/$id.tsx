import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/a/program/$id')({
  component: () => <div>Hello /a/program/$id!</div>
})