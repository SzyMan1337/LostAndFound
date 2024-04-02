import { ApiLoginResponse } from "./types/apiLogin";
import { ApiRegisterResponse } from "./types/apiRegister";

export function AccountLogin(request) {
  return new ApiLoginResponse();
}
export function AccountRegister(request) {
  return new ApiRegisterResponse();
}
