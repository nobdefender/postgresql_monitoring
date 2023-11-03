export type AuthTokens = {
  accessToken: string;
  refreshToken: string;
};

export type User = {
  id: number;
  emailAddress: string;
  name: string;
  lastName: string;
  username: string;
  role: string;
};
