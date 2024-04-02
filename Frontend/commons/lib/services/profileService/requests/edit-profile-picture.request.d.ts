import { ProfileResponseType } from "../profileTypes";
export declare const editProfilePhoto: (photo: {
    name: string | null;
    type: string | null;
    uri: string;
}, accessToken: string) => Promise<ProfileResponseType | undefined>;
