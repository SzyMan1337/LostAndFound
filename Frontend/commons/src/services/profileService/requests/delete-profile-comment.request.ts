import { http } from "../../../http";
import {
  ProfileCommentRequestType,
  ProfileCommentFromServerType,
} from "../profileCommentTypes";

export const deleteProfileComment = async (
  userId: string,
  accessToken: string
): Promise<boolean> => {
  const result = await http<
    ProfileCommentFromServerType,
    ProfileCommentRequestType
  >({
    path: `/profile/${userId}/comments`,
    method: "delete",
    accessToken,
  });

  if (result.ok) {
    return true;
  } else {
    return false;
  }
};
