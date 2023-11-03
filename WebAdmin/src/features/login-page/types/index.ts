export type User = {
  id: number;
  emailAddress: string;
  name: string;
  lastName: string;
  username: string;
  role: string;
};

export type UserResponse = {
  jwt: string;
  user: User;
};

export type RegisterCommand = {
  email: string;
  userName: string;
  password: string;
};

export type LoginCredentials = {
  username: string;
  password: string;
};

export type AuthTokens = {
  accessToken: string;
  refreshToken: string;
};

export type UserLoginResponse = AuthTokens & {
  user: User;
};
