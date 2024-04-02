import { http } from "../../../http";
import { mapLoginFromServer, } from "../loginTypes";
export const login = async (user) => {
    const result = await http({
        path: "/account/login",
        method: "post",
        body: user,
    });
    if (result.ok && result.body) {
        return mapLoginFromServer(result.body);
    }
    else {
        return undefined;
    }
};
