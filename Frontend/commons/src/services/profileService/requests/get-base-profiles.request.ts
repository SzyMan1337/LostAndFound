import { http } from "../../../http";
import { BaseProfileType } from "../profileTypes";

export const getBaseProfiles = async (
  userIds: string[],
  accessToken: string
): Promise<BaseProfileType[]> => {
  if (userIds.length < 0) {
    return [];
  }
  let path: string = "/profile/list?";
  for (const userId of userIds) {
    path += `&userIds=${userId}`;
  }

  const result = await http<BaseProfileType[]>({
    path,
    method: "get",
    accessToken,
  });

  if (result.ok && result.body) {
    return result.body;
  } else {
    return [];
  }
};
