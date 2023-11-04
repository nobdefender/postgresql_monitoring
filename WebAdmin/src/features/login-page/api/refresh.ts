import { axios } from '../../../lib/axios';
import { AuthTokens } from '../types';

export const refreshAccessToken = (request: AuthTokens): Promise<AuthTokens> => {
  return axios.post('/webUser/refresh', request);
};
