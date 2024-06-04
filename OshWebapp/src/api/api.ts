import axios from "axios";
import { AuthResponse } from "../models/auth/AuthResponse.ts";

export const open = axios.create({
  timeout: 10000,
  headers: { accept: "application/json", "Content-Type": "application/json" },
});

export const auth = () =>
  axios.create({
    timeout: 10000,
    headers: {
      accept: "application/json",
      "Content-Type": "application/json",
      Authorization: `Bearer ${getToken()}`,
    },
  });

export const authFileSend = () =>
  axios.create({
    timeout: 10000,
    headers: {
      accept: "application/json",
      "Content-Type": "multipart/form-data",
      Authorization: `Bearer ${getToken()}`,
    },
  });

function getToken() {
  const item = localStorage.getItem("auth");

  if (item) {
    const user: AuthResponse = JSON.parse(item);

    return user.jwtToken;
  }
}

export function getName() {
  const item = localStorage.getItem("auth");

  if (item) {
    const user: AuthResponse = JSON.parse(item);
    if (user.user && user.user.firstName && user.user.lastName && user.user.middleName) {
      return `${user.user.lastName} ${user.user.firstName} ${user.user.middleName} `;
    }
  }
}

export function exit() {
  localStorage.removeItem("auth");
}
