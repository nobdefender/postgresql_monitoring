import { MantineProvider } from '@mantine/core';
import { PropsWithChildren } from 'react';

type AppProviderProps = PropsWithChildren;

export const AppProvider: React.FC<AppProviderProps> = ({ children }) => {
  return <MantineProvider>{children}</MantineProvider>;
};
