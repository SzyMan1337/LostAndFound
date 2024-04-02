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

export const mapMessageFromServer = (
  message: MessageFromServerResponseType
): MessageResponseType => ({
  ...message,
  creationTime: new Date(message.creationTime),
});
