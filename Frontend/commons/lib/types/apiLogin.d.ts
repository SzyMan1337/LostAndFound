export declare class ApiLoginRequest {
    email: string;
    password: string;
}
export declare class ApiLoginResponse {
    accessToken: string;
    accessTokenExpirationTime: Date;
    refreshToken: string;
}
