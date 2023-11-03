import { Center, Flex, Title } from '@mantine/core';
import { UserBlock } from './UserBlock';

export const MainPage: React.FC = () => {
  return (
    <>
      <Center>
        <Title>Управление доступами пользователей</Title>
      </Center>
      <Flex mt={20} w="100%">
        <UserBlock />
      </Flex>
    </>
  );
};
