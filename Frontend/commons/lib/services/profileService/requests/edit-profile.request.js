import { http } from "../../../http";
export const editProfile = async (profile, accessToken) => {
    const result = await http({
        path: "/profile",
        method: "put",
        body: profile,
        accessToken,
    });
    if (result.ok && result.body) {
        return result.body;
    }
    else {
        return undefined;
    }
};
