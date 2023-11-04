import { WebUser } from '@/features/login-page/types';
import { axios } from '@/lib/axios';
import { ExtractFnReturnType } from '@/lib/react-query';
import { UseQueryOptions, useQuery } from '@tanstack/react-query';
import { TelegramBotUser } from '../../types';

export const getAllTelegramBotUsers = (): Promise<TelegramBotUser[]> => {
  return axios.get('/telegramBotUser/getAllTelegramBotUsers');
};

type QueryFnType = typeof getAllTelegramBotUsers;
type UseGetAllTelegramBotUsersRequest = {
  config?: UseQueryOptions<TelegramBotUser[]>;
};

export const useAllTelegramBotUsers = ({ config }: UseGetAllTelegramBotUsersRequest = {}) => {
  return useQuery<ExtractFnReturnType<QueryFnType>>({
    queryKey: ['get-all-telegram-bot-users'],
    queryFn: () => getAllTelegramBotUsers(),
    ...config,
  });
};
