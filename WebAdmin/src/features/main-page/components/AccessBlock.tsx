import { Box, Flex, Paper, Text, Checkbox, Stack, Button } from '@mantine/core';
import { useAllActions } from '../api/action/allActions';
import { useEffect, useState } from 'react';
import { Action } from '../types';
import { useUserActions } from '../api/action/userActions';
import { User } from '@/features/login-page/types';
import isNil from 'lodash-es/isNil';
import orderBy from 'lodash-es/orderBy';
import find from 'lodash-es/find';

type AccessBlockProps = {
  user?: User;
};

export const AccessBlock: React.FC<AccessBlockProps> = ({ user }) => {
  const [isEditing, setIsEditing] = useState(false);
  const [localUserActions, setLocalUserActions] = useState<(Action & { isSelected: boolean })[]>();

  const { data: allActions } = useAllActions();
  const { data: userActions } = useUserActions({
    request: {
      userId: user?.id as number,
    },
    config: {
      enabled: !isNil(user?.id),
    },
  });

  useEffect(() => {
    const value = orderBy(
      allActions?.map((item) => ({
        ...item,
        isSelected: userActions?.map(({ name }) => name).includes(item.name) ?? false,
      })),
      'name'
    );
    setLocalUserActions(value);
  }, [userActions]);

  return (
    <Paper p="lg" w={500} h={500} withBorder>
      <Stack h="100%" justify="space-between">
        <Stack>
          {localUserActions?.map(({ name, isSelected }) => (
            <Flex key={`${name}${isSelected}`} gap="md" align="center">
              <Checkbox
                disabled={!isEditing}
                checked={isSelected}
                onChange={({ currentTarget }) => {
                  const prevValue = find(localUserActions, { name });
                  if (!isNil(prevValue)) {
                    setLocalUserActions((prev) =>
                      prev?.map((item) =>
                        item.name === name
                          ? { ...prevValue, isSelected: currentTarget.checked }
                          : item
                      )
                    );
                  }
                }}
              />
              <Text>{name}</Text>
            </Flex>
          ))}
        </Stack>
        <Flex justify="flex-end">
          <Button
            w={140}
            variant={isEditing ? 'outline' : 'filled'}
            onClick={() => setIsEditing((prev) => !prev)}
          >
            {isEditing ? 'Сохранить' : 'Редактировать'}
          </Button>
        </Flex>
      </Stack>
    </Paper>
  );
};
