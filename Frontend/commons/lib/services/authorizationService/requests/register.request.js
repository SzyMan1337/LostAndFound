import { http } from "../../../http";
export const register = async (user) => {
    const result = await http({
        path: "/account/register",
        method: "post",
        body: user,
    });
    if (result.ok && result.body) {
        return { ok: true, body: result.body };
    }
    else {
        return { ok: false, errors: result.errors };
    }
};
