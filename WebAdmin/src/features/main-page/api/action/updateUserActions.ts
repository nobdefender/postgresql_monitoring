import { axios } from '@/lib/axios';
import { MutationConfig } from '@/lib/react-query';
import { useMutation } from '@tanstack/react-query';
import { UpdateUserActionsRequest } from '../../types';

export const updateUserActions = (request: UpdateUserActionsRequest): Promise<boolean> => {
  return axios.put('/action/updateUserActions', request);
};

type UseUpdateUserActionsRequest = {
  config?: MutationConfig<typeof updateUserActions>;
};

export const useUpdateUserActions = ({ config }: UseUpdateUserActionsRequest = {}) => {
  return useMutation({
    mutationKey: ['update-user-actions'],
    mutationFn: updateUserActions,
    ...config,
  });
};
