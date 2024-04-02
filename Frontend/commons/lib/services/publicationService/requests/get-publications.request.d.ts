import { PaginationMetadata } from "../../../http";
import { PublicationResponseType, PublicationSearchRequestType, PublicationSortType } from "../publicationTypes";
export declare const getPublications: (pageNumber: number, accessToken: string, publication?: PublicationSearchRequestType, orderBy?: {
    firstArgumentSort: PublicationSortType;
    secondArgumentSort: PublicationSortType;
}) => Promise<{
    pagination?: PaginationMetadata | undefined;
    publications: PublicationResponseType[];
}>;
