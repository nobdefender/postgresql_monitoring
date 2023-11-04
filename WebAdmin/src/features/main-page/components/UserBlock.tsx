import { Flex, Paper, Select, Title, Text, Box } from '@mantine/core';
import find from 'lodash-es/find';
import { useAllTelegramBotUsers } from '../api/user/allUsers';
import { TelegramBotUser } from '../types';

type UserBlockProps = {
  telegramUser?: TelegramBotUser;
  setTelegramUser: (value?: TelegramBotUser) => void;
};

export const UserBlock: React.FC<UserBlockProps> = ({ telegramUser, setTelegramUser }) => {
  const { data: allTelegramBotUsers } = useAllTelegramBotUsers();

  return (
    <Paper p="lg" w={500} h={500} withBorder>
      <Select
        value={telegramUser?.telegramChatId?.toString()}
        placeholder="Выберите пользователя"
        data={allTelegramBotUsers?.map(({ telegramChatId }) => `${telegramChatId}`)}
        onChange={(value) => {
          const newValue = find(allTelegramBotUsers, { telegramChatId: Number(value) });
          setTelegramUser(newValue);
        }}
      />
      <Box mt={20}>
        <Flex>
          <Title order={5}>Id пользователя:&nbsp;</Title>
          <Text>{telegramUser?.id ?? '-'}</Text>
        </Flex>
        <Flex>
          <Title order={5}>Id чата:&nbsp;</Title>
          <Text>{telegramUser?.telegramChatId ?? '-'}</Text>
        </Flex>
      </Box>
    </Paper>
  );
};
