import { http, PaginationMetadata } from "../../../http";
import {
  mapMessageFromServer,
  MessageFromServerResponseType,
  MessageResponseType,
} from "../messageTypes";

export const getChatMessages = async (
  recipentId: string,
  accessToken: string,
  pageNumber?: number
): Promise<{
  pagination?: PaginationMetadata;
  messages: MessageResponseType[];
}> => {
  const result = await http<MessageFromServerResponseType[]>({
    path: `/chat/message/${recipentId}${
      pageNumber ? `?pageNumber=${pageNumber}` : ""
    }`,
    method: "get",
    accessToken,
  });

  const pagination = result.headers?.get("X-Pagination");
  if (result.ok && result.body && pagination) {
    return {
      pagination: JSON.parse(pagination),
      messages: result.body.map(mapMessageFromServer),
    };
  } else if (result.ok && result.body) {
    return {
      messages: result.body.map(mapMessageFromServer),
    };
  } else {
    return { messages: [] };
  }
};
