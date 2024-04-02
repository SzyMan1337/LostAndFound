import { MessageRequestType, MessageResponseType } from "../messageTypes";
export declare const addChatMessage: (recipentId: string, message: MessageRequestType, accessToken: string) => Promise<MessageResponseType | undefined>;
