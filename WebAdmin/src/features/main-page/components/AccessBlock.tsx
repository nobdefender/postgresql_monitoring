import { Box, Flex, Paper, Text, Checkbox, Stack, Button } from '@mantine/core';
import { useAllActions } from '../api/action/allActions';
import { useEffect, useState } from 'react';
import { Action, TelegramBotUser } from '../types';
import { useUserActions } from '../api/action/userActions';
import { WebUser } from '@/features/login-page/types';
import isNil from 'lodash-es/isNil';
import orderBy from 'lodash-es/orderBy';
import find from 'lodash-es/find';
import { useUpdateUserActions } from '../api/action/updateUserActions';

type AccessBlockProps = {
  telegramUser?: TelegramBotUser;
};

export const AccessBlock: React.FC<AccessBlockProps> = ({ telegramUser }) => {
  const [isEditing, setIsEditing] = useState(false);
  const [localUserActions, setLocalUserActions] = useState<(Action & { isSelected: boolean })[]>();

  const { data: allActions } = useAllActions();
  const { data: userActions } = useUserActions({
    request: {
      userId: telegramUser?.id as number,
    },
    config: {
      enabled: !isNil(telegramUser?.id),
    },
  });

  const { mutate: updateUserActions } = useUpdateUserActions();

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
            disabled={isNil(telegramUser?.id)}
            variant={isEditing ? 'outline' : 'filled'}
            onClick={() =>
              setIsEditing((prev) => {
                if (prev) {
                  updateUserActions({
                    telegramBotUserId: telegramUser?.id as number,
                    actionIds:
                      localUserActions
                        ?.filter(({ isSelected }) => isSelected)
                        ?.map(({ actionid }) => actionid) ?? [],
                  });
                }
                return !prev;
              })
            }
          >
            {isEditing ? 'Сохранить' : 'Редактировать'}
          </Button>
        </Flex>
      </Stack>
    </Paper>
  );
};
