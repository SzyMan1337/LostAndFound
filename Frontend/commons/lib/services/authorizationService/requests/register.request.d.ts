import { RegisterErrorType, RegisterRequestType, RegisterResponseType } from "../registerTypes";
export declare const register: (user: RegisterRequestType) => Promise<{
    ok: boolean;
    body?: RegisterResponseType;
    errors?: RegisterErrorType;
}>;
