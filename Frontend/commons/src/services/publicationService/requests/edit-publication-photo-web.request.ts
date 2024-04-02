import { multipartFormDataHttp } from "../../../http";
import {
	PublicationResponseType,
	PublicationFromServerType,
	mapPublicationFromServer,
} from "../publicationTypes";

export const editPublicationPhotoWeb = async (
	publicationId: string,
	photo: File,
	accessToken: string
): Promise<PublicationResponseType | undefined> => {
	const data: FormData = new FormData();
	data.append(
		"photo",
		photo
	);
	const result = await multipartFormDataHttp<
		PublicationFromServerType,
		string
	>(
		{
			path: `/publication/${publicationId}/photo`,
			method: "PATCH",
			accessToken,
		},
		data
	);

	if (result.ok && result.body) {
		return mapPublicationFromServer(result.body);
	} else {
		return undefined;
	}
};
