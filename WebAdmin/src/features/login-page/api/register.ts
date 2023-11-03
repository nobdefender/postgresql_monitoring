import axios from "axios";
import { UserResponse } from "../types";

export type RegisterCredentials = {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
};

export const registerWithEmailAndPassword = (
  data: RegisterCredentials
): Promise<UserResponse> => {
  return axios.post("/auth/register", data);
};
