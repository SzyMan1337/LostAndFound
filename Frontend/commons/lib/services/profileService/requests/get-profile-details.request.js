import { http } from "../../../http";
export const getProfileDetails = async (userId, accessToken) => {
    const result = await http({
        path: `/profile/${userId}`,
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
