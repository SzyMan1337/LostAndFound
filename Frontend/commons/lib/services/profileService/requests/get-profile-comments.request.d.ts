import { PaginationMetadata } from "../../../http";
import { ProfileCommentsSectionResponseType } from "../profileCommentTypes";
export declare const getProfileComments: (userId: string, accessToken: string, pageNumber?: number) => Promise<{
    pagination?: PaginationMetadata | undefined;
    commentsSection: ProfileCommentsSectionResponseType;
} | undefined>;
