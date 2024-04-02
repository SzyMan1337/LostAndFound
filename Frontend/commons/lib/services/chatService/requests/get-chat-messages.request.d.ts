import { PaginationMetadata } from "../../../http";
import { MessageResponseType } from "../messageTypes";
export declare const getChatMessages: (recipentId: string, accessToken: string, pageNumber?: number) => Promise<{
    pagination?: PaginationMetadata | undefined;
    messages: MessageResponseType[];
}>;
