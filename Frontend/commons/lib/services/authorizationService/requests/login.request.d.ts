import { LoginRequestType, LoginResponseType } from "../loginTypes";
export declare const login: (user: LoginRequestType) => Promise<LoginResponseType | undefined>;
