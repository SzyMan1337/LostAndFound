import { multipartFormDataHttp } from "../../../http";
import {
  PublicationResponseType,
  PublicationFromServerType,
  mapPublicationFromServer,
} from "../publicationTypes";

export const editPublicationPhoto = async (
  publicationId: string,
  photo: { name: string | null; type: string | null; uri: string },
  accessToken: string
): Promise<PublicationResponseType | undefined> => {
  const data: FormData = new FormData();
  data.append(
    "photo",
    JSON.parse(
      JSON.stringify({
        name: photo.name,
        type: photo.type,
        uri: photo.uri,
      })
    )
  );
  const result = await multipartFormDataHttp<PublicationFromServerType, string>(
    {
      path: `/publication/${publicationId}/photo`,
      method: "PATCH",
      accessToken,
    },
    data
  );

  if (result.ok && result.body) {
    return mapPublicationFromServer(result.body);
  } else {
    return undefined;
  }
};
