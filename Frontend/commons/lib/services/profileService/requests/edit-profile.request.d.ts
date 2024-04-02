import { ProfileRequestType, ProfileResponseType } from "../profileTypes";
export declare const editProfile: (profile: ProfileRequestType, accessToken: string) => Promise<ProfileResponseType | undefined>;
