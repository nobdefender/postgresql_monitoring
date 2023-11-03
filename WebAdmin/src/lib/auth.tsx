import { configureAuth } from 'react-query-auth';
import { storage } from '@/utils/storage';
import { LoginCredentials, User, UserLoginResponse } from '@/features/login-page/types';
import { getUser } from '@/features/login-page/api/getUser';
import { loginWithLoginAndPassword } from '@/features/login-page/api/login';
import {
  RegisterCredentials,
  registerWithEmailAndPassword,
} from '@/features/login-page/api/register';

function handleUserResponse(data: UserLoginResponse) {
  const { accessToken, refreshToken, user } = data;
  storage.setToken('accessToken', accessToken);
  storage.setToken('refreshToken', refreshToken);
  return user;
}

async function userFn(): Promise<User | null> {
  const accessToken: string = storage.getToken('accessToken');
  if (accessToken) {
    return (await getUser(accessToken)) ?? null;
  }

  return null;
}

async function loginFn(data: LoginCredentials) {
  const response = await loginWithLoginAndPassword(data);
  handleUserResponse(response);
  return response.user;
}

async function registerFn(data: RegisterCredentials) {
  const response = await registerWithEmailAndPassword(data);
  storage.setToken('accessToken', response.jwt);
  return response.user;
}

async function logoutFn() {
  storage.clearToken('accessToken');
  storage.clearToken('refreshToken');
}

export const { useUser, useLogin, useRegister, useLogout, AuthLoader } = configureAuth<
  User | null,
  unknown,
  LoginCredentials,
  RegisterCredentials
>({
  userFn,
  logoutFn,
  registerFn,
  loginFn,
});
