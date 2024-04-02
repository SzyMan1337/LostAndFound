import { PublicationResponseType } from "../publicationTypes";
export declare const editPublicationPhoto: (publicationId: string, photo: {
    name: string | null;
    type: string | null;
    uri: string;
}, accessToken: string) => Promise<PublicationResponseType | undefined>;
