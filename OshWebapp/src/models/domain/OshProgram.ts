import { Specialty } from "./Specialty.ts";

export interface OshProgram {
  id: number;
  name: string;
  description: string;
  learningMinutesDuration: number;
  trainingMinutesDuration: number;
  specialty: Specialty;
  specialityId: number;
  autoAssignmentType: string;
  maxAutoAssignments: number;
  trainingSuccessRate: number;
}
