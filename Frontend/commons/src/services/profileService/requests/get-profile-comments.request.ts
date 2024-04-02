import { http, PaginationMetadata } from "../../../http";
import {
  mapProfileCommentsSectionFromServer,
  ProfileCommentsSectionFromServerType,
  ProfileCommentsSectionResponseType,
} from "../profileCommentTypes";

export const getProfileComments = async (
  userId: string,
  accessToken: string,
  pageNumber: number = 1
): Promise<
  | {
      pagination?: PaginationMetadata;
      commentsSection: ProfileCommentsSectionResponseType;
    }
  | undefined
> => {
  const result = await http<ProfileCommentsSectionFromServerType>({
    path: `/profile/${userId}/comments?pageNumber=${pageNumber}`,
    method: "get",
    accessToken,
  });

  const pagination = result.headers?.get("X-Pagination");
  if (result.ok && result.body && pagination) {
    return {
      pagination: JSON.parse(pagination),
      commentsSection: mapProfileCommentsSectionFromServer(result.body),
    };
  } else if (result.ok && result.body) {
    return {
      commentsSection: mapProfileCommentsSectionFromServer(result.body),
    };
  } else {
    return undefined;
  }
};
