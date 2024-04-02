import { http } from "../../../http";
import { ProfileResponseType } from "../profileTypes";

export const deleteProfilePhoto = async (
  accessToken: string
): Promise<boolean> => {
  const result = await http<ProfileResponseType>({
    path: `/profile/picture`,
    method: "delete",
    accessToken,
  });

  if (result.ok) {
    return true;
  } else {
    return false;
  }
};
