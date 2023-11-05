export type Action = {
  id: number;
  name: string;
  description: string;
};

export type UpdateUserActionsRequest = {
  telegramBotUserId: number;
  ids: number[];
};

export type TelegramBotUser = {
  id: number;
  telegramChatId: number;
  firstName: string;
  lastName: string;
  userName: string;
};
