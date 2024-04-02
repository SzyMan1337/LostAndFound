import { multipartFormDataHttp } from "../../../http";
import { mapPublicationFromServer, } from "../publicationTypes";
export const editPublicationPhotoWeb = async (publicationId, photo, accessToken) => {
    const data = new FormData();
    data.append("photo", photo);
    const result = await multipartFormDataHttp({
        path: `/publication/${publicationId}/photo`,
        method: "PATCH",
        accessToken,
    }, data);
    if (result.ok && result.body) {
        return mapPublicationFromServer(result.body);
    }
    else {
        return undefined;
    }
};
