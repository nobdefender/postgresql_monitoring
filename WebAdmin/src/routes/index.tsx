import { LayoutBody } from '@/components/layout/LayoutBody';
import { LayoutHeader } from '@/components/layout/LayoutHeader';
import { LoginPage } from '@/features/login-page/components/LoginPage';
import { storage } from '@/utils/storage';
import isNil from 'lodash-es/isNil';
import { Navigate, Route, Routes } from 'react-router-dom';

export const AppRoutes = () => {
  // const { data: userResponse } = useUser();
  // const { setUser } = useAuthenticationContext();

  // useEffect(() => {
  //   if (!isNil(userResponse)) {А
  //     setUser(userResponse);
  //   }
  // }, [setUser, userResponse]);

  return (
    <>
      <LayoutHeader />
      <LayoutBody>
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route path="*" element={<Navigate to="/main" replace />} />
          {isNil(storage.getToken('accessToken')) && (
            <Route path="*" element={<Navigate to="/login" replace />} />
          )}
        </Routes>
      </LayoutBody>
    </>
  );
};
