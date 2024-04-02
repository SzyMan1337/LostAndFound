import { http } from "../../../http";
import {
  PublicationRequestType,
  PublicationResponseType,
  PublicationFromServerType,
  mapPublicationFromServer,
} from "../publicationTypes";

export const getPublication = async (
  publicationId: string,
  accessToken: string
): Promise<PublicationResponseType | undefined> => {
  const result = await http<PublicationFromServerType>({
    path: `/publication/${publicationId}`,
    method: "get",
    accessToken,
  });

  if (result.ok && result.body) {
    return mapPublicationFromServer(result.body);
  } else {
    return undefined;
  }
};
