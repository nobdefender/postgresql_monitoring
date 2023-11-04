import { AppShell, Flex, Menu, Stack, Text, Title } from '@mantine/core';
import classes from './LayoutHeader.module.css';
import { useAuthenticationContext } from '@/providers/Authentication.context';
import { IconChevronDown, IconLogout } from '@tabler/icons-react';
import { useLogout } from '@/lib/auth';
import { useNavigate } from 'react-router-dom';

export const LayoutHeader: React.FC = () => {
  const logout = useLogout();
  const navigate = useNavigate();
  const { user, setUser } = useAuthenticationContext();

  return (
    <AppShell header={{ height: 60 }}>
      <AppShell.Header>
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
              <Stack align="center" my="md">
                <Text ta="center">
                  <div>
                    {user?.name} {user?.lastName}
                  </div>
                </Text>
              </Stack>
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
      </AppShell.Header>
    </AppShell>
  );
};
