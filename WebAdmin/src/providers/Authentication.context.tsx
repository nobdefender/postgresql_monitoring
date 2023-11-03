import { User } from '@/features/login-page/types';
import noop from 'lodash-es/noop';
import { PropsWithChildren, createContext, useContext, useState } from 'react';

type AuthenticationContextValue = {
  user: User | null;
  setUser: (value: User | null) => void;
};

const AuthenticationContext = createContext<AuthenticationContextValue>({
  user: null,
  setUser: noop,
});

export const AuthenticationContextProvider: React.FC<PropsWithChildren> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);
  return (
    <AuthenticationContext.Provider
      value={{
        user,
        setUser,
      }}
    >
      {children}
    </AuthenticationContext.Provider>
  );
};

// eslint-disable-next-line react-refresh/only-export-components
export const useAuthenticationContext = () => useContext(AuthenticationContext);
