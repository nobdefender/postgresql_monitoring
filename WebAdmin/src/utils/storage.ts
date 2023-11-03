export const storage = {
  getToken: (key: string) => {
    return JSON.parse(window.localStorage.getItem(key) as string);
  },
  setToken: (key: string, token: string) => {
    window.localStorage.setItem(key, JSON.stringify(token));
  },
  clearToken: (key: string) => {
    window.localStorage.removeItem(key);
  },
};
