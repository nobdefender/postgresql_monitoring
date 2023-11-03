import { Divider } from '@mantine/core';
import { Button, Flex, Paper, PasswordInput, TextInput, Title } from '@mantine/core';
import Logo128 from '@/public/logo_128.svg';

export const LoginPage: React.FC = () => {
  return (
    <Flex bgsz="contain" bg="url(https://synergy.ru/assets/upload/news/academy/task1.jpg)" h="100%">
      <Paper w={500} h="100%" radius={0} p={30}>
        <Flex justify="center">
          <img src={Logo128}></img>
        </Flex>
        <Title mt="md" mb={50} order={2} ta="center">
          Мониторинг PostgreSQL
        </Title>
        <form
        // onSubmit={form.onSubmit(async (values) => {
        //   await login.mutateAsync(values, {
        //     onSuccess: () => navigate('/tasks'),
        //   });
        // })}
        >
          <TextInput
            label="Логин"
            placeholder="Введите почту"
            size="md"
            autoComplete="on"
            // {...form.getInputProps('username')}
          />
          <PasswordInput
            label="Пароль"
            placeholder="Введите пароль"
            mt="md"
            size="md"
            autoComplete="on"
            // {...form.getInputProps('password')}
          />
          <Button fullWidth mt="xl" size="md" type="submit">
            Войти
          </Button>
          {/* {login.data === null && (
            <Text pt="sm"  color="red">
              Неверная комбинация логина и пароля
            </Text>
          )} */}
        </form>
      </Paper>
      <Divider orientation="vertical" />
    </Flex>
  );
};
