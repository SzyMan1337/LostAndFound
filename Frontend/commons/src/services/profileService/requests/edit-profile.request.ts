import { http } from "../../../http";
import { ProfileRequestType, ProfileResponseType } from "../profileTypes";

export const editProfile = async (
  profile: ProfileRequestType,
  accessToken: string
): Promise<ProfileResponseType | undefined> => {
  const result = await http<ProfileResponseType, ProfileRequestType>({
    path: "/profile",
    method: "put",
    body: profile,
    accessToken,
  });

  if (result.ok && result.body) {
    return result.body;
  } else {
    return undefined;
  }
};
