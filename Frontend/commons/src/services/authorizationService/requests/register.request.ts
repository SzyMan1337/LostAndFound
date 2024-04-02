import { http } from "../../../http";
import {
  RegisterErrorType,
  RegisterRequestType,
  RegisterResponseType,
} from "../registerTypes";

export const register = async (
  user: RegisterRequestType
): Promise<{
  ok: boolean;
  body?: RegisterResponseType;
  errors?: RegisterErrorType;
}> => {
  const result = await http<
    RegisterResponseType,
    RegisterRequestType,
    RegisterErrorType
  >({
    path: "/account/register",
    method: "post",
    body: user,
  });
  if (result.ok && result.body) {
    return { ok: true, body: result.body };
  } else {
    return { ok: false, errors: result.errors };
  }
};
