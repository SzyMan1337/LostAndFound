import { http } from "../../../http";

export const logout = async (): Promise<boolean | undefined> => {
  const result = await http({
    path: "/account/logout",
    method: "delete",
  });

  if (result.ok && result.body) {
    return true;
  } else {
    return undefined;
  }
};
