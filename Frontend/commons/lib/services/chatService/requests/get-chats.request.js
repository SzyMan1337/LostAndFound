import { http } from "../../../http";
export const getChats = async (accessToken, pageNumber) => {
    var _a;
    const result = await http({
        path: `/chat${pageNumber ? `?pageNumber=${pageNumber}` : ""}`,
        method: "get",
        accessToken,
    });
    const pagination = (_a = result.headers) === null || _a === void 0 ? void 0 : _a.get("X-Pagination");
    if (result.ok && result.body && pagination) {
        return {
            pagination: JSON.parse(pagination),
            chats: result.body,
        };
    }
    else if (result.ok && result.body) {
        return {
            chats: result.body,
        };
    }
    else {
        return { chats: [] };
    }
};
