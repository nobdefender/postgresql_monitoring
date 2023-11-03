import { axios } from '@/lib/axios';
import { User } from '../types';

export const getUser = (token: string): Promise<User> => {
  return axios.get('/user/getUserData', {
    params: {
      token,
    },
  });
};
