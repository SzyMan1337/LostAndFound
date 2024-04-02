import { http } from "../../../http";
import { mapProfileCommentFromServer, } from "../profileCommentTypes";
export const addProfileComment = async (userId, comment, accessToken) => {
    const result = await http({
        path: `/profile/${userId}/comments`,
        method: "post",
        body: comment,
        accessToken,
    });
    if (result.ok && result.body) {
        return mapProfileCommentFromServer(result.body);
    }
    else {
        return undefined;
    }
};
