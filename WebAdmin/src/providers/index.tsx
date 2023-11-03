import { Loader, MantineProvider, Stack } from '@mantine/core';
import { PropsWithChildren } from 'react';
import { AuthenticationContextProvider } from './Authentication.context';
import { queryClient } from '@/lib/react-query';
import { QueryClientProvider } from '@tanstack/react-query';
import React from 'react';

type AppProviderProps = PropsWithChildren;

export const AppProvider: React.FC<AppProviderProps> = ({ children }) => {
  return (
    <React.Suspense
      fallback={
        <Stack align="center" justify="center">
          <Loader size="xl" />
        </Stack>
      }
    >
      <QueryClientProvider client={queryClient}>
        <MantineProvider>
          <AuthenticationContextProvider>{children}</AuthenticationContextProvider>
        </MantineProvider>
      </QueryClientProvider>
    </React.Suspense>
  );
};
