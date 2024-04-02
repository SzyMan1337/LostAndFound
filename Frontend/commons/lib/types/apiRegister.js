export class ApiRegisterRequest {
    constructor() {
        this.email = "";
        this.username = "";
        this.password = "";
        this.confirmPassword = "";
    }
}
export class ApiRegisterResponse {
    constructor() {
        this.userIdentifier = "";
        this.email = "";
        this.username = "";
    }
}
