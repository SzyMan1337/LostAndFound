export type LoginRequestType = {
    email: string;
    password: string;
};
export type LoginResponseType = {
    accessToken: string;
    accessTokenExpirationTime: Date;
    refreshToken: string;
};
export type LoginFromServerType = {
    accessToken: string;
    accessTokenExpirationTime: string;
    refreshToken: string;
};
export declare const mapLoginFromServer: (data: LoginFromServerType) => LoginResponseType;
export type EditPwdRequestType = {
    Password: string;
    NewPassword: string;
};
