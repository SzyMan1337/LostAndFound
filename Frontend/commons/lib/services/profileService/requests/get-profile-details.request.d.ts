import { ProfileResponseType } from "../profileTypes";
export declare const getProfileDetails: (userId: string, accessToken: string) => Promise<ProfileResponseType | undefined>;
