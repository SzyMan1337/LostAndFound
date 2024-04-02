import { http } from "../../../http";
export const getBaseProfiles = async (userIds, accessToken) => {
    if (userIds.length < 0) {
        return [];
    }
    let path = "/profile/list?";
    for (const userId of userIds) {
        path += `&userIds=${userId}`;
    }
    const result = await http({
        path,
        method: "get",
        accessToken,
    });
    if (result.ok && result.body) {
        return result.body;
    }
    else {
        return [];
    }
};
