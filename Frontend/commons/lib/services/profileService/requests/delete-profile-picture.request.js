import { http } from "../../../http";
export const deleteProfilePhoto = async (accessToken) => {
    const result = await http({
        path: `/profile/picture`,
        method: "delete",
        accessToken,
    });
    if (result.ok) {
        return true;
    }
    else {
        return false;
    }
};
