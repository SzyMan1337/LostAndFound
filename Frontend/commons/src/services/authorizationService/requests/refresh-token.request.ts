import { http } from "../../../http";
import {
  LoginResponseType,
  LoginFromServerType,
  mapLoginFromServer,
} from "../loginTypes";

export const refreshToken = async (
  token: string
): Promise<LoginResponseType | undefined> => {
  const result = await http<LoginFromServerType, { refreshToken: string }>({
    path: "/account/refresh",
    method: "post",
    body: {
      refreshToken: token,
    },
  });

  if (result.ok && result.body) {
    return mapLoginFromServer(result.body);
  } else {
    return undefined;
  }
};
