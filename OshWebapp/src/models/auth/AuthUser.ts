export interface AuthUser {
  id: number;
  login: string;
  type: "Admin" | "Employee";
  firstName: string;
  middleName: string;
  lastName: string;
}
