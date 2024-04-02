import { http } from "../../../http";
export const readChat = async (chatMemberId, accessToken) => {
    const result = await http({
        path: `/chat/${chatMemberId}`,
        method: "PATCH",
        accessToken,
    });
    if (result.ok) {
        return true;
    }
    else {
        return false;
    }
};
