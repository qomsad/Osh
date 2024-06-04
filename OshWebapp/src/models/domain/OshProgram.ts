import { Specialty } from "./Specialty.ts";

export interface OshProgram {
  id: number;
  name: string;
  description: string;
  learningMinutesDuration: number;
  trainingMinutesDuration: number;
  specialty: Specialty;
  specialtyId: number;
  autoAssignmentType: "FullManual";
  maxAutoAssignments: number;
  trainingSuccessRate: number;
}
