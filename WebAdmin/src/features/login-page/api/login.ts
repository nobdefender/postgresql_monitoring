import { axios } from '../../../lib/axios';
import { LoginCredentials, UserLoginResponse } from '../types';

export const loginWithLoginAndPassword = (data: LoginCredentials): Promise<UserLoginResponse> => {
  return axios.post('/webUser/login', data);
};
