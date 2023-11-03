import { Center, Flex, Stack, Title } from '@mantine/core';
import { UserBlock } from './UserBlock';
import { AccessBlock } from './AccessBlock';
import { useState } from 'react';
import { User } from '@/features/login-page/types';

export const MainPage: React.FC = () => {
  const [user, setUser] = useState<User>();

  return (
    <Stack gap="lg">
      <Center>
        <Title>Управление доступами пользователей</Title>
      </Center>
      <Center>
        <Flex mt={20} w="min-content" gap="xl">
          <UserBlock user={user} setUser={setUser} />
          <AccessBlock user={user} />
        </Flex>
      </Center>
    </Stack>
  );
};
