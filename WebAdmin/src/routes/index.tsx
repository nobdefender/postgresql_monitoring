import { LayoutBody } from '@/components/layout/LayoutBody';
import { LayoutHeader } from '@/components/layout/LayoutHeader';
import { LoginPage } from '@/features/login-page/components/LoginPage';
import { MainPage } from '@/features/main-page/components/MainPage';
import { useUser } from '@/lib/auth';
import { useAuthenticationContext } from '@/providers/Authentication.context';
import { storage } from '@/utils/storage';
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
    <>
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
          {isNil(storage.getToken('accessToken')) && (
            <Route path="*" element={<Navigate to="/login" replace />} />
          )}
        </Routes>
      </LayoutBody>
    </>
  );
};
