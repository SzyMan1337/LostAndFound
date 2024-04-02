import { http } from "../../../http";
import {
  mapMessageFromServer,
  MessageFromServerResponseType,
  MessageRequestType,
  MessageResponseType,
} from "../messageTypes";

export const addChatMessage = async (
  recipentId: string,
  message: MessageRequestType,
  accessToken: string
): Promise<MessageResponseType | undefined> => {
  const result = await http<MessageFromServerResponseType, MessageRequestType>({
    path: `/chat/message/${recipentId}`,
    body: message,
    method: "post",
    accessToken,
  });

  if (result.ok && result.body) {
    return mapMessageFromServer(result.body);
  } else {
    return undefined;
  }
};
