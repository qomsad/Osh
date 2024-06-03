import { AuthUser } from "./AuthUser.ts";

export interface AuthResponse {
  jwtToken: string;
  user: AuthUser;
}
