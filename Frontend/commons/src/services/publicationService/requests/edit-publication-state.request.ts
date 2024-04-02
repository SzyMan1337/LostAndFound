import { http } from "../../../http";
import {
  PublicationResponseType,
  PublicationFromServerType,
  mapPublicationFromServer,
  PublicationState,
} from "../publicationTypes";

export const editPublicationState = async (
  publicationId: string,
  state: PublicationState,
  accessToken: string
): Promise<PublicationResponseType | undefined> => {
  const result = await http<
    PublicationFromServerType,
    { publicationState: PublicationState }
  >({
    path: `/publication/${publicationId}`,
    method: "PATCH",
    body: { publicationState: state },
    accessToken,
  });

  if (result.ok && result.body) {
    return mapPublicationFromServer(result.body);
  } else {
    return undefined;
  }
};
