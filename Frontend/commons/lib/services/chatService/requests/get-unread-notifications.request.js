import { http } from "../../../http";
export const getUnreadNotifications = async (accessToken) => {
    const result = await http({
        path: "/chat/notification",
        method: "get",
        accessToken,
    });
    if (result.ok && result.body) {
        return result.body;
    }
    else {
        return undefined;
    }
};
