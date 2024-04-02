import { MessageResponseType } from "../chatService/messageTypes";

export type ProfileRequestType = {
  name?: string;
  surname?: string;
  description?: string;
  city?: string;
};

export type ProfileResponseType = {
  userId: string;
  email?: string;
  username?: string;
  name?: string;
  surname?: string;
  description?: string;
  city?: string;
  pictureUrl?: string;
  averageProfileRating: number;
};

export type AuthorResponseType = {
  id: string;
  username?: string;
  pictureUrl?: string;
};

export type BaseProfileType = {
  userId: string;
  username?: string;
  pictureUrl?: string;
};

export type BaseProfileChatType = {
  userId: string;
  username?: string;
  pictureUrl?: string;
  chatId: string;
  containsUnreadMessage: boolean;
  lastMessage: MessageResponseType;
};
