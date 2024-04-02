import { PublicationRequestType, PublicationResponseType } from "../publicationTypes";
export declare const addPublication: (publication: PublicationRequestType, accessToken: string, photo?: {
    name: string | null;
    type: string | null;
    uri: string;
}, filePhoto?: File) => Promise<PublicationResponseType | undefined>;
