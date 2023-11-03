import { User } from '@/features/login-page/types';
import { axios } from '@/lib/axios';
import { ExtractFnReturnType } from '@/lib/react-query';
import { UseQueryOptions, useQuery } from '@tanstack/react-query';
import { Action } from '../../types';

export const getUserActions = (userId: { userId: number }): Promise<Action[]> => {
  return axios.get('/action/userActions', { params: userId });
};

type QueryFnType = typeof getUserActions;
type UseGetUserActionRequest = {
  request: {
    userId: number;
  };
  config?: UseQueryOptions<Action[]>;
};

export const useUserActions = ({ request, config }: UseGetUserActionRequest) => {
  return useQuery<ExtractFnReturnType<QueryFnType>>({
    queryKey: ['get-user-actions', request],
    queryFn: () => getUserActions(request),
    ...config,
  });
};
