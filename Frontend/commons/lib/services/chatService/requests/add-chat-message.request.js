import { http } from "../../../http";
import { mapMessageFromServer, } from "../messageTypes";
export const addChatMessage = async (recipentId, message, accessToken) => {
    const result = await http({
        path: `/chat/message/${recipentId}`,
        body: message,
        method: "post",
        accessToken,
    });
    if (result.ok && result.body) {
        return mapMessageFromServer(result.body);
    }
    else {
        return undefined;
    }
};
