import { MessageResponseType } from "./messageTypes";
export type ChatBaseResponseType = {
    chatId: string;
    containsUnreadMessage: boolean;
    lastMessage: MessageResponseType;
    chatMember: ChatUserType;
};
export type ChatUserType = {
    id: string;
};
export type ChatNotificationResponseType = {
    unreadChatsCount: number;
    unreadMessageSenders: ChatUserType[];
};
