import { Center, Flex, Stack, Title } from '@mantine/core';
import { UserBlock } from './UserBlock';
import { AccessBlock } from './AccessBlock';
import { useState } from 'react';
import { WebUser } from '@/features/login-page/types';
import { TelegramBotUser } from '../types';

export const MainPage: React.FC = () => {
  const [telegramUser, setTelegramUser] = useState<TelegramBotUser>();

  return (
    <Stack gap="lg">
      <Center>
        <Title>Управление доступами пользователей</Title>
      </Center>
      <Center>
        <Flex mt={20} w="min-content" gap="xl">
          <UserBlock telegramUser={telegramUser} setTelegramUser={setTelegramUser} />
          <AccessBlock telegramUser={telegramUser} />
        </Flex>
      </Center>
    </Stack>
  );
};
