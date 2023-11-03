import Axios from 'axios';
import isNil from 'lodash-es/isNil';
import { storage } from '@/utils/storage';
import { refreshAccessToken } from '@/features/login-page/api/refresh';

export const axios = Axios.create({
  baseURL: import.meta.env.VITE_BACKEND_API_URL,
});

axios.interceptors.request.use(
  (config) => {
    const accessToken = storage.getToken('accessToken');
    if (accessToken) {
      config.headers.authorization = `Bearer ${accessToken}`;
    }
    config.headers.Accept = 'application/json';
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalConfig = error.config;
    if (error.response.status === 401 && !originalConfig._retry) {
      originalConfig._retry = true;
      try {
        const refreshTokenResponse = await refreshAccessToken({
          accessToken: storage.getToken('accessToken'),
          refreshToken: storage.getToken('refreshToken'),
        });

        const { accessToken, refreshToken } = refreshTokenResponse;
        storage.setToken('accessToken', accessToken);
        storage.setToken('refreshToken', refreshToken);
        error.config.headers['Authorization'] = 'Bearer ' + accessToken;
        if (!isNil(originalConfig?.params?.token)) {
          originalConfig.params.token = accessToken;
        }
        return axios.request(originalConfig);
      } catch (_error) {
        return Promise.reject(_error);
      }
    }

    return Promise.reject(error);
  }
);

axios.interceptors.response.use((response) => {
  return response.data;
});
