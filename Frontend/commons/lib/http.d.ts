export interface HttpRequest<REQB> {
    path: string;
    method?: string;
    body?: REQB;
    accessToken?: string;
    contentType?: string;
}
export interface HttpResponse<RESB, RESE> {
    ok: boolean;
    body?: RESB;
    headers?: Headers;
    errors?: RESE;
}
export interface PaginationMetadata {
    TotalItemCount: number;
    TotalPageCount: number;
    PageSize: number;
    CurrentPage: number;
    NextPageLink?: string;
    PreviousPageLink?: string;
}
export declare const http: <RESB = undefined, REQB = undefined, RESE = undefined>(config: HttpRequest<REQB>) => Promise<HttpResponse<RESB, RESE>>;
export declare const multipartFormDataHttp: <RESB = undefined, REQB = undefined, RESE = undefined>(config: HttpRequest<REQB>, requestData: FormData) => Promise<HttpResponse<RESB, RESE>>;
