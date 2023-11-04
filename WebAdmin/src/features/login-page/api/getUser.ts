import { axios } from '@/lib/axios';
import { WebUser } from '../types';

export const getUser = (token: string): Promise<WebUser> => {
  return axios.get('/webUser/getUserData', {
    params: {
      token,
    },
  });
};
