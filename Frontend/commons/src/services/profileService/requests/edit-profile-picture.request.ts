import { multipartFormDataHttp } from "../../../http";
import { ProfileResponseType } from "../profileTypes";

export const editProfilePhoto = async (
  photo: { name: string | null; type: string | null; uri: string },
  accessToken: string
): Promise<ProfileResponseType | undefined> => {
  const data: FormData = new FormData();
  data.append(
    "picture",
    JSON.parse(
      JSON.stringify({
        name: photo.name,
        type: photo.type,
        uri: photo.uri,
      })
    )
  );
  const result = await multipartFormDataHttp<ProfileResponseType, string>(
    {
      path: "/profile/picture",
      method: "PATCH",
      accessToken,
    },
    data
  );

  if (result.ok && result.body) {
    return result.body;
  } else {
    return undefined;
  }
};
