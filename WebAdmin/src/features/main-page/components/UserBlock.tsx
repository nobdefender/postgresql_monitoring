import { Flex, Paper, Select, Title, Text, Box } from '@mantine/core';
import { useAllUsers } from '../api/user/allUsers';
import { useState } from 'react';
import { User } from '@/features/login-page/types';
import find from 'lodash-es/find';

type UserBlockProps = {
  user?: User;
  setUser: (value?: User) => void;
};

export const UserBlock: React.FC<UserBlockProps> = ({ user, setUser }) => {
  const { data: allUsers } = useAllUsers();

  return (
    <Paper p="lg" w={500} h={500} withBorder>
      <Select
        value={user?.username}
        placeholder="Выберите пользователя"
        data={allUsers?.map(({ username }) => `${username}`)}
        onChange={(value) => {
          const newValue = find(allUsers, { username: value as string });
          setUser(newValue);
        }}
      />
      <Box mt={20}>
        <Flex>
          <Title order={5}>Id пользователя:&nbsp;</Title>
          <Text>{user?.id ?? '-'}</Text>
        </Flex>
        <Flex>
          <Title order={5}>Логин:&nbsp;</Title>
          <Text>{user?.username ?? '-'}</Text>
        </Flex>
        <Flex>
          <Title order={5}>Имя:&nbsp;</Title>
          <Text>{user?.name ?? '-'}</Text>
        </Flex>
        <Flex>
          <Title order={5}>Фамилия:&nbsp;</Title>
          <Text>{user?.lastName ?? '-'}</Text>
        </Flex>
        <Flex>
          <Title order={5}>Email:&nbsp;</Title>
          <Text>{user?.emailAddress ?? '-'}</Text>
        </Flex>
      </Box>
    </Paper>
  );
};
