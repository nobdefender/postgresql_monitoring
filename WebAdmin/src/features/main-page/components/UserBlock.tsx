import { Flex, Paper, Select, Title, Text, Box } from '@mantine/core';
import find from 'lodash-es/find';
import { useAllTelegramBotUsers } from '../api/user/allUsers';
import { TelegramBotUser } from '../types';
import isNil from 'lodash-es/isNil';

type UserBlockProps = {
  telegramUser?: TelegramBotUser;
  setTelegramUser: (value?: TelegramBotUser) => void;
};

export const UserBlock: React.FC<UserBlockProps> = ({ telegramUser, setTelegramUser }) => {
  const { data: allTelegramBotUsers } = useAllTelegramBotUsers();

  return (
    <Paper p="lg" w={500} h={500} withBorder>
      <Select
        value={telegramUser?.userName}
        placeholder="Выберите пользователя"
        data={allTelegramBotUsers?.map(({ userName }) => userName) ?? []}
        onChange={(value) => {
          const newValue = find(allTelegramBotUsers, { userName: value as string });
          if (!isNil(newValue)) {
            setTelegramUser(newValue);
          }
        }}
      />
      <Box mt={20}>
        <Flex>
          <Title order={5}>Id пользователя:&nbsp;</Title>
          <Text>{telegramUser?.id ?? '-'}</Text>
        </Flex>
        <Flex>
          <Title order={5}>Имя:&nbsp;</Title>
          <Text>{telegramUser?.firstName ?? '-'}</Text>
        </Flex>
        <Flex>
          <Title order={5}>Фамилия:&nbsp;</Title>
          <Text>{telegramUser?.lastName ?? '-'}</Text>
        </Flex>
        <Flex>
          <Title order={5}>Ник:&nbsp;</Title>
          <Text>{telegramUser?.userName ?? '-'}</Text>
        </Flex>
        <Flex>
          <Title order={5}>Id чата:&nbsp;</Title>
          <Text>{telegramUser?.telegramChatId ?? '-'}</Text>
        </Flex>
      </Box>
    </Paper>
  );
};
