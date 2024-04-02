import { multipartFormDataHttp } from "../../../http";
import { mapPublicationFromServer, } from "../publicationTypes";
export const addPublication = async (publication, accessToken, photo, filePhoto) => {
    const data = new FormData();
    if (publication.title)
        data.append("title", publication.title);
    if (publication.description)
        data.append("description", publication.description);
    if (publication.incidentAddress)
        data.append("incidentAddress", publication.incidentAddress);
    if (publication.incidentDate)
        data.append("incidentDate", publication.incidentDate.toDateString());
    if (publication.publicationType)
        data.append("publicationType", publication.publicationType);
    if (publication.subjectCategoryId)
        data.append("subjectCategoryId", publication.subjectCategoryId);
    if (photo)
        data.append("subjectPhoto", JSON.parse(JSON.stringify({
            name: photo.name,
            type: photo.type,
            uri: photo.uri,
        })));
    else if (filePhoto) {
        data.append("photo", filePhoto);
    }
    const result = await multipartFormDataHttp({
        path: "/publication",
        method: "post",
        contentType: "multipart/form-data; boundary=------WebKitFormBoundary2lZSUsxEA3X5jpYD",
        accessToken,
    }, data);
    if (result.ok && result.body) {
        return mapPublicationFromServer(result.body);
    }
    else {
        return undefined;
    }
};
