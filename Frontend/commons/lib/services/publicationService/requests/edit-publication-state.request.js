import { http } from "../../../http";
import { mapPublicationFromServer, } from "../publicationTypes";
export const editPublicationState = async (publicationId, state, accessToken) => {
    const result = await http({
        path: `/publication/${publicationId}`,
        method: "PATCH",
        body: { publicationState: state },
        accessToken,
    });
    if (result.ok && result.body) {
        return mapPublicationFromServer(result.body);
    }
    else {
        return undefined;
    }
};
