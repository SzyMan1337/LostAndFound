import { http } from "../../../http";
import {
  LoginRequestType,
  LoginResponseType,
  LoginFromServerType,
  mapLoginFromServer,
} from "../loginTypes";

export const login = async (
  user: LoginRequestType
): Promise<LoginResponseType | undefined> => {
  const result = await http<LoginFromServerType, LoginRequestType>({
    path: "/account/login",
    method: "post",
    body: user,
  });

  if (result.ok && result.body) {
    return mapLoginFromServer(result.body);
  } else {
    return undefined;
  }
};
