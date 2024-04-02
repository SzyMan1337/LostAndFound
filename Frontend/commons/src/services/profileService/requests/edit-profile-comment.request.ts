import { http } from "../../../http";
import {
  ProfileCommentRequestType,
  ProfileCommentResponseType,
  ProfileCommentFromServerType,
  mapProfileCommentFromServer,
} from "../profileCommentTypes";

export const editProfileComment = async (
  userId: string,
  comment: ProfileCommentRequestType,
  accessToken: string
): Promise<ProfileCommentResponseType | undefined> => {
  const result = await http<
    ProfileCommentFromServerType,
    ProfileCommentRequestType
  >({
    path: `/profile/${userId}/comments`,
    method: "put",
    body: comment,
    accessToken,
  });

  if (result.ok && result.body) {
    return mapProfileCommentFromServer(result.body);
  } else {
    return undefined;
  }
};
