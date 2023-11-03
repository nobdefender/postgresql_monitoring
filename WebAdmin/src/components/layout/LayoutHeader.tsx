import { AppShell } from '@mantine/core';

export const LayoutHeader: React.FC = () => {
  // const { user, setUser } = useAuthenticationContext();

  return (
    <AppShell header={{ height: 60 }}>
      <AppShell.Header></AppShell.Header>
    </AppShell>
  );
};
