export class ApiLoginRequest {
    constructor() {
        this.email = "";
        this.password = "";
    }
}
export class ApiLoginResponse {
    constructor() {
        this.accessToken = "";
        this.accessTokenExpirationTime = new Date();
        this.refreshToken = "";
    }
}
