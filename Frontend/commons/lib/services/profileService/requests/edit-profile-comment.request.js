import { http } from "../../../http";
import { mapProfileCommentFromServer, } from "../profileCommentTypes";
export const editProfileComment = async (userId, comment, accessToken) => {
    const result = await http({
        path: `/profile/${userId}/comments`,
        method: "put",
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
