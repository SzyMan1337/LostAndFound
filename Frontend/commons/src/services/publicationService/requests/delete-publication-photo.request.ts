import { http } from "../../../http";
import { PublicationFromServerType } from "../publicationTypes";

export const deletePublicationPhoto = async (
  publicationId: string,
  accessToken: string
): Promise<boolean> => {
  const result = await http<PublicationFromServerType>({
    path: `/publication/${publicationId}/photo`,
    method: "delete",
    accessToken,
  });

  if (result.ok) {
    return true;
  } else {
    return false;
  }
};
