export type MessageRequestType = {
    content: string;
};
export type MessageResponseType = {
    content: string;
    creationTime: Date;
    authorId: string;
};
export type MessageFromServerResponseType = {
    content: string;
    creationTime: string;
    authorId: string;
};
export declare const mapMessageFromServer: (message: MessageFromServerResponseType) => MessageResponseType;
