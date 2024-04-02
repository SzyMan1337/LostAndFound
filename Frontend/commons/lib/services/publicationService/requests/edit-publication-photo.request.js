import { multipartFormDataHttp } from "../../../http";
import { mapPublicationFromServer, } from "../publicationTypes";
export const editPublicationPhoto = async (publicationId, photo, accessToken) => {
    const data = new FormData();
    data.append("photo", JSON.parse(JSON.stringify({
        name: photo.name,
        type: photo.type,
        uri: photo.uri,
    })));
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
