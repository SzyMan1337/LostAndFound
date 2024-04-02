import { http } from "../../../http";
export const getProfile = async (accessToken) => {
    const result = await http({
        path: "/profile",
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
