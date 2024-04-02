import { http } from "../../../http";
import {
	LoginRequestType,
	LoginFromServerType,
	mapLoginFromServer,
	EditPwdRequestType,
} from "../loginTypes";

export const changePwd = async (
	accessToken: string,
	pwd: EditPwdRequestType
): Promise<boolean | undefined> => {
	const result = await http<EditPwdRequestType, EditPwdRequestType>({
		path: "/account/password",
		method: "PUT",
		body: pwd,
		accessToken,
	});

	if (result.ok) {
		return true;
	} else {
		return undefined;
	}
};
