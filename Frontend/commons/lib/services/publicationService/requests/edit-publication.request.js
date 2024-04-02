import { http } from "../../../http";
import { mapPublicationFromServer, } from "../publicationTypes";
export const editPublication = async (publicationId, publication, accessToken) => {
    const result = await http({
        path: `/publication/${publicationId}`,
        method: "put",
        body: publication,
        accessToken,
    });
    if (result.ok && result.body) {
        return mapPublicationFromServer(result.body);
    }
    else {
        return undefined;
    }
};
