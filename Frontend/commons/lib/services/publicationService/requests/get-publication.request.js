import { http } from "../../../http";
import { mapPublicationFromServer, } from "../publicationTypes";
export const getPublication = async (publicationId, accessToken) => {
    const result = await http({
        path: `/publication/${publicationId}`,
        method: "get",
        accessToken,
    });
    if (result.ok && result.body) {
        return mapPublicationFromServer(result.body);
    }
    else {
        return undefined;
    }
};
