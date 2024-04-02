import { PaginationMetadata } from "../../../http";
import { ChatBaseResponseType } from "../chatTypes";
export declare const getChats: (accessToken: string, pageNumber?: number) => Promise<{
    pagination?: PaginationMetadata | undefined;
    chats: ChatBaseResponseType[];
}>;
