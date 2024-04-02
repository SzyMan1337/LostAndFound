import { http } from "../../../http";
export const getCategories = async (accessToken) => {
    const result = await http({
        path: "/publication/categories",
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
