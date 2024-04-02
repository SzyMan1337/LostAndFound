import { http } from "../../../http";
import { mapMessageFromServer, } from "../messageTypes";
export const getChatMessages = async (recipentId, accessToken, pageNumber) => {
    var _a;
    const result = await http({
        path: `/chat/message/${recipentId}${pageNumber ? `?pageNumber=${pageNumber}` : ""}`,
        method: "get",
        accessToken,
    });
    const pagination = (_a = result.headers) === null || _a === void 0 ? void 0 : _a.get("X-Pagination");
    if (result.ok && result.body && pagination) {
        return {
            pagination: JSON.parse(pagination),
            messages: result.body.map(mapMessageFromServer),
        };
    }
    else if (result.ok && result.body) {
        return {
            messages: result.body.map(mapMessageFromServer),
        };
    }
    else {
        return { messages: [] };
    }
};
