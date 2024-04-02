import { PublicationResponseType, SinglePublicationVote } from "../publicationTypes";
export declare const editPublicationRating: (publicationId: string, rating: SinglePublicationVote, accessToken: string) => Promise<PublicationResponseType | undefined>;
