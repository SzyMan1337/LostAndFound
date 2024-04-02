import { http } from "../../../http";
import { mapLoginFromServer, } from "../loginTypes";
export const refreshToken = async (token) => {
    const result = await http({
        path: "/account/refresh",
        method: "post",
        body: {
            refreshToken: token,
        },
    });
    if (result.ok && result.body) {
        return mapLoginFromServer(result.body);
    }
    else {
        return undefined;
    }
};
