import { http } from "../../../http";
import { mapProfileCommentsSectionFromServer, } from "../profileCommentTypes";
export const getProfileComments = async (userId, accessToken, pageNumber = 1) => {
    var _a;
    const result = await http({
        path: `/profile/${userId}/comments?pageNumber=${pageNumber}`,
        method: "get",
        accessToken,
    });
    const pagination = (_a = result.headers) === null || _a === void 0 ? void 0 : _a.get("X-Pagination");
    if (result.ok && result.body && pagination) {
        return {
            pagination: JSON.parse(pagination),
            commentsSection: mapProfileCommentsSectionFromServer(result.body),
        };
    }
    else if (result.ok && result.body) {
        return {
            commentsSection: mapProfileCommentsSectionFromServer(result.body),
        };
    }
    else {
        return undefined;
    }
};
