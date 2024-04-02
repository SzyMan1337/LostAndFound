import { http } from "../../../http";
export const changePwd = async (accessToken, pwd) => {
    const result = await http({
        path: "/account/password",
        method: "PUT",
        body: pwd,
        accessToken,
    });
    if (result.ok) {
        return true;
    }
    else {
        return undefined;
    }
};
