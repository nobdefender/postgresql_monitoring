import { User } from '@/features/login-page/types';
import { axios } from '@/lib/axios';
import { ExtractFnReturnType } from '@/lib/react-query';
import { UseQueryOptions, useQuery } from '@tanstack/react-query';
import { Action } from '../../types';

export const getAllActions = (): Promise<Action[]> => {
  return axios.get('/action/allActions');
};

type QueryFnType = typeof getAllActions;
type UseGetAllActionRequest = {
  config?: UseQueryOptions<Action[]>;
};

export const useAllActions = ({ config }: UseGetAllActionRequest = {}) => {
  return useQuery<ExtractFnReturnType<QueryFnType>>({
    queryKey: ['get-all-actions'],
    queryFn: () => getAllActions(),
    ...config,
  });
};
