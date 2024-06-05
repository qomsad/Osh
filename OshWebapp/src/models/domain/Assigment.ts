import { OshProgram } from "./OshProgram.ts";
import { Employee } from "./Employee.ts";

export interface Assigment {
  id: number;
  employee: Employee;
  userEmployeeId: number;
  oshProgram: OshProgram;
  oshProgramId: number;
  assignmentDate: string;
  startLearning?: string;
  startTraining?: string;
  result?: Result;
}

export interface Result {
  id: number;
  learningResult: number;
  trainingResult: number;
  timestamp: string;
}
