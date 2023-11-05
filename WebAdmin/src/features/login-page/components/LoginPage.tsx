import { Divider } from '@mantine/core';
import { Button, Flex, Paper, PasswordInput, TextInput, Title, Text } from '@mantine/core';
import Logo128 from '@/public/logo_128.svg';
import { useForm } from '@mantine/form';
import { LoginCredentials } from '../types';
import { useLogin } from '@/lib/auth';
import { useNavigate } from 'react-router-dom';
import { useAuthenticationContext } from '@/providers/Authentication.context';
import isNil from 'lodash-es/isNil';
import { useEffect } from 'react';

export const LoginPage: React.FC = () => {
  const login = useLogin();
  const navigate = useNavigate();
  const form = useForm({
    initialValues: {
      username: '',
      password: '',
    } as LoginCredentials,
  });
  const { user } = useAuthenticationContext();

  useEffect(() => {
    if (!isNil(user)) {
      navigate('/main');
    }
  }, []);

  return (
    <Flex bg="url(./admin-preview.png)" h="100%">
      <Paper w={500} h="100%" radius={0} p={30}>
        <Flex justify="center">
          <img src={Logo128}></img>
        </Flex>
        <Title mt="md" mb={50} order={2} ta="center">
          Мониторинг PostgreSQL
        </Title>
        <form
          onSubmit={form.onSubmit(async (values) => {
            await login.mutateAsync(values, {
              onSuccess: () => navigate('/main'),
            });
          })}
        >
          <TextInput
            label="Логин"
            placeholder="Введите почту"
            size="md"
            autoComplete="on"
            {...form.getInputProps('username')}
          />
          <PasswordInput
            label="Пароль"
            placeholder="Введите пароль"
            mt="md"
            size="md"
            autoComplete="on"
            {...form.getInputProps('password')}
          />
          <Button fullWidth mt="xl" size="md" type="submit">
            Войти
          </Button>
          {/* {login.data == null && (
            <Text pt="sm" c="red">
              Неверная комбинация логина и пароля
            </Text>
          )} */}
        </form>
      </Paper>
      <Divider orientation="vertical" />
    </Flex>
  );
};
