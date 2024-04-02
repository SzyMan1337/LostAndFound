import { multipartFormDataHttp } from "../../../http";
export const editProfilePhotoWeb = async (photo, accessToken) => {
    const data = new FormData();
    data.append("picture", photo);
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
