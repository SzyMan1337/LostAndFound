import { http, PaginationMetadata } from "../../../http";
import { ChatBaseResponseType } from "../chatTypes";

export const getChats = async (
  accessToken: string,
  pageNumber?: number
): Promise<{
  pagination?: PaginationMetadata;
  chats: ChatBaseResponseType[];
}> => {
  const result = await http<ChatBaseResponseType[]>({
    path: `/chat${pageNumber ? `?pageNumber=${pageNumber}` : ""}`,
    method: "get",
    accessToken,
  });

  const pagination = result.headers?.get("X-Pagination");
  if (result.ok && result.body && pagination) {
    return {
      pagination: JSON.parse(pagination),
      chats: result.body,
    };
  } else if (result.ok && result.body) {
    return {
      chats: result.body,
    };
  } else {
    return { chats: [] };
  }
};
