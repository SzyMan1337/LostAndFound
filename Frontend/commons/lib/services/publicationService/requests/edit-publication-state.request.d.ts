import { PublicationResponseType, PublicationState } from "../publicationTypes";
export declare const editPublicationState: (publicationId: string, state: PublicationState, accessToken: string) => Promise<PublicationResponseType | undefined>;
