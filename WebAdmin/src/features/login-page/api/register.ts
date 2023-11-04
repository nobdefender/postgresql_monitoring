import axios from "axios";
import { WebUserResponse } from "../types";

export type RegisterCredentials = {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
};

export const registerWithEmailAndPassword = (
  data: RegisterCredentials
): Promise<WebUserResponse> => {
  return axios.post("/auth/register", data);
};
