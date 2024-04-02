import { LoginResponseType } from "../loginTypes";
export declare const refreshToken: (token: string) => Promise<LoginResponseType | undefined>;
