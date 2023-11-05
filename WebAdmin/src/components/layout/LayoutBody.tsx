import { useAuthenticationContext } from '@/providers/Authentication.context';
import { Anchor, AppShell, Box, Flex, Stack } from '@mantine/core';
import { IconArrowLeft } from '@tabler/icons-react';
import isNil from 'lodash-es/isNil';
import { PropsWithChildren } from 'react';
import { useMatch, useNavigate } from 'react-router-dom';
import classes from './LayoutBody.module.css';

type LayoutBodyProps = PropsWithChildren;

export const LayoutBody: React.FC<LayoutBodyProps> = ({ children }) => {
  const { user } = useAuthenticationContext();
  const navigate = useNavigate();
  const isMainRoute = useMatch('/main');

  return (
    <AppShell.Main h="100%">
      <Stack h="100%" bg="gray.1">
        {!isNil(user) ? (
          <>
            <Box w="70%" my="xl" mx="auto">
              {!isMainRoute && (
                <Anchor onClick={() => navigate(-1)}>
                  <Flex mb="lg" gap="xs" align="center">
                    <IconArrowLeft size={16} className={classes.backIcon} />
                    Назад
                  </Flex>
                </Anchor>
              )}
              {children}
            </Box>
          </>
        ) : (
          children
        )}
      </Stack>
    </AppShell.Main>
  );
};
