import { AppShell, Box, Flex, Menu, Stack, Text, Title } from '@mantine/core';
import classes from './LayoutHeader.module.css';
import { useAuthenticationContext } from '@/providers/Authentication.context';
import { IconChevronDown, IconLogout } from '@tabler/icons-react';
import { useLogout } from '@/lib/auth';
import { useNavigate } from 'react-router-dom';
import isNil from 'lodash-es/isNil';

export const LayoutHeader: React.FC = () => {
  const logout = useLogout();
  const navigate = useNavigate();
  const { user, setUser } = useAuthenticationContext();

  return (
    <AppShell header={{ height: 60 }}>
      <AppShell.Header>
        <Flex h="100%" justify="space-between" align="center">
          <Title pl="xl" order={3}>
            PostgreSQL Monitoring Tool Admin
          </Title>
          {!isNil(user) && (
            <Flex pr={120} h="100%" align="center" justify="flex-end">
              <Menu offset={25} withinPortal width={260}>
                <Menu.Target>
                  <Flex gap="sm" align="center" className={classes.menuTarget}>
                    <Flex align="center" gap="xs">
                      <Text fw={600}>
                        {user?.name} {user?.lastName}
                      </Text>
                      <IconChevronDown size={14} />
                    </Flex>
                  </Flex>
                </Menu.Target>
                <Menu.Dropdown>
                  <Box p="lg">
                    <Text ta="center">
                      {user?.name} {user?.lastName}
                    </Text>
                    <Title ta="center" order={5}>
                      {user?.role === 'Admin' ? 'Администратор' : 'Пользователь'}
                    </Title>
                  </Box>

                  <Menu.Label>Настройки</Menu.Label>
                  <Menu.Item
                    leftSection={<IconLogout size={14} stroke={1.5} />}
                    onClick={() => {
                      setUser(null);
                      logout.mutate({});
                      navigate('/login');
                    }}
                  >
                    Выйти
                  </Menu.Item>
                </Menu.Dropdown>
              </Menu>
            </Flex>
          )}
        </Flex>
      </AppShell.Header>
    </AppShell>
  );
};
