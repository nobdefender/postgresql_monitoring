import { User } from '@/features/login-page/types';
import { axios } from '@/lib/axios';
import { ExtractFnReturnType } from '@/lib/react-query';
import { UseQueryOptions, useQuery } from '@tanstack/react-query';

export const getAllUsers = (): Promise<User[]> => {
  return axios.get('/user/allUsers');
};

type QueryFnType = typeof getAllUsers;
type UseGetAllUsersRequest = {
  config?: UseQueryOptions<User[]>;
};

export const useAllUsers = ({ config }: UseGetAllUsersRequest = {}) => {
  return useQuery<ExtractFnReturnType<QueryFnType>>({
    queryKey: ['get-all-users'],
    queryFn: () => getAllUsers(),
    ...config,
  });
};
