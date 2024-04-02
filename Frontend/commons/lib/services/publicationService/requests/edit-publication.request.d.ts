import { PublicationRequestType, PublicationResponseType } from "../publicationTypes";
export declare const editPublication: (publicationId: string, publication: PublicationRequestType, accessToken: string) => Promise<PublicationResponseType | undefined>;
