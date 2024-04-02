import { EditPwdRequestType } from "../loginTypes";
export declare const changePwd: (accessToken: string, pwd: EditPwdRequestType) => Promise<boolean | undefined>;
