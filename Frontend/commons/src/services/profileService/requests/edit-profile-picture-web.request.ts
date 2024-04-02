import { multipartFormDataHttp } from "../../../http";
import { ProfileResponseType } from "../profileTypes";

export const editProfilePhotoWeb = async (
	photo: File ,
	accessToken: string
): Promise<ProfileResponseType | undefined> => {
	const data: FormData = new FormData();
	data.append("picture", photo);

	const result = await multipartFormDataHttp<ProfileResponseType, string>(
		{
			path: "/profile/picture",
			method: "PATCH",
			accessToken,
		},
		data
	);

	if (result.ok && result.body) {
		return result.body;
	} else {
		return undefined;
	}
};
