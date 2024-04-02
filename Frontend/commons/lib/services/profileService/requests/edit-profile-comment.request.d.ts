import { ProfileCommentRequestType, ProfileCommentResponseType } from "../profileCommentTypes";
export declare const editProfileComment: (userId: string, comment: ProfileCommentRequestType, accessToken: string) => Promise<ProfileCommentResponseType | undefined>;
