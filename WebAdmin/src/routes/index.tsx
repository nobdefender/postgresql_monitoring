import { LayoutBody } from '@/components/layout/LayoutBody';
import { LayoutFooter } from '@/components/layout/LayoutFooter';
import { LayoutHeader } from '@/components/layout/LayoutHeader';
import { LoginPage } from '@/features/login-page/components/LoginPage';
import { MainPage } from '@/features/main-page/components/MainPage';
import { useUser } from '@/lib/auth';
import { useAuthenticationContext } from '@/providers/Authentication.context';
import { storage } from '@/utils/storage';
import { AppShell } from '@mantine/core';
import isNil from 'lodash-es/isNil';
import { useEffect } from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';

export const AppRoutes = () => {
  const { data: userResponse } = useUser();
  const { user, setUser } = useAuthenticationContext();

  useEffect(() => {
    if (!isNil(userResponse)) {
      setUser(userResponse);
    }
  }, [setUser, userResponse]);

  return (
    <AppShell header={{ height: 60 }} footer={{ height: 60 }}>
      <LayoutHeader />
      <LayoutBody>
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route
            path="/main"
            element={
              !isNil(user) || !isNil(storage.getToken('accessToken')) ? (
                <MainPage />
              ) : (
                <Navigate to="/login" replace />
              )
            }
          />
          <Route path="*" element={<Navigate to="/main" replace />} />
          {/* {isNil(storage.getToken('accessToken')) && (
            <Route path="*" element={<Navigate to="/login" replace />} />
          )} */}
        </Routes>
      </LayoutBody>
      <LayoutFooter />
    </AppShell>
  );
};
