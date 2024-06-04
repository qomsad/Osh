import { Specialty } from "./Specialty.ts";

export interface Employee {
  id: number;
  type: "Admin" | "Employee";
  login: string;
  firstName: string;
  middleName: string;
  lastName: string;
  serviceNumber: string;
  specialty: Specialty;
  specialityId: number;
  password?: string;
}
