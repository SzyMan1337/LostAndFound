import { multipartFormDataHttp } from "../../../http";
export const editProfilePhoto = async (photo, accessToken) => {
    const data = new FormData();
    data.append("picture", JSON.parse(JSON.stringify({
        name: photo.name,
        type: photo.type,
        uri: photo.uri,
    })));
    const result = await multipartFormDataHttp({
        path: "/profile/picture",
        method: "PATCH",
        accessToken,
    }, data);
    if (result.ok && result.body) {
        return result.body;
    }
    else {
        return undefined;
    }
};
