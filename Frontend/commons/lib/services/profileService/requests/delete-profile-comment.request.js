import { http } from "../../../http";
export const deleteProfileComment = async (userId, accessToken) => {
    const result = await http({
        path: `/profile/${userId}/comments`,
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
