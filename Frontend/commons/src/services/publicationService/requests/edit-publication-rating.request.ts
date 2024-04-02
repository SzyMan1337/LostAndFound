import { http } from "../../../http";
import {
  PublicationResponseType,
  SinglePublicationVote,
  PublicationFromServerType,
  mapPublicationFromServer,
} from "../publicationTypes";

export const editPublicationRating = async (
  publicationId: string,
  rating: SinglePublicationVote,
  accessToken: string
): Promise<PublicationResponseType | undefined> => {
  const result = await http<
    PublicationFromServerType,
    { newPublicationVote: SinglePublicationVote }
  >({
    path: `/publication/${publicationId}/rating`,
    method: "PATCH",
    body: { newPublicationVote: rating },
    accessToken,
  });

  if (result.ok && result.body) {
    return mapPublicationFromServer(result.body);
  } else {
    return undefined;
  }
};
