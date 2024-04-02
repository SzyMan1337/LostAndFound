import { webAPIUrl } from "./AppSettings";

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

export const http = async <
  RESB = undefined,
  REQB = undefined,
  RESE = undefined
>(
  config: HttpRequest<REQB>
): Promise<HttpResponse<RESB, RESE>> => {
  const request = new Request(`${webAPIUrl}${config.path}`, {
    method: config.method || "get",
    headers: {
      "Content-Type": config.contentType
        ? config.contentType
        : "application/json",
    },
    body: config.body ? JSON.stringify(config.body) : undefined,
  });
  if (config.accessToken) {
    request.headers.set("Authorization", `Bearer ${config.accessToken}`);
  }

  const response = await fetch(request);
  if (response.ok) {
    try {
      const body = await response.json();
      const headers = response.headers;
      return { ok: response.ok, body, headers };
    } catch {
      return { ok: response.ok };
    }
  } else {
    try {
      const body = await response.json();
      logErrorBody(request, body);
      return { ok: response.ok, errors: body.errors };
    } catch {
      logError(request, response);
      return { ok: response.ok };
    }
  }
};

export const multipartFormDataHttp = async <
  RESB = undefined,
  REQB = undefined,
  RESE = undefined
>(
  config: HttpRequest<REQB>,
  requestData: FormData
): Promise<HttpResponse<RESB, RESE>> => {
  const request = new Request(`${webAPIUrl}${config.path}`, {
    method: config.method || "get",
    body: requestData,
  });
  if (config.accessToken) {
    request.headers.set("Authorization", `Bearer ${config.accessToken}`);
  }

  const response = await fetch(request);
  if (response.ok) {
    try {
      const body = await response.json();
      const headers = response.headers;
      return { ok: response.ok, body, headers };
    } catch {
      return { ok: response.ok };
    }
  } else {
    try {
      const body = await response.json();
      logErrorBody(request, body);
      return { ok: response.ok, errors: body.errors };
    } catch {
      logError(request, response);
      return { ok: response.ok };
    }
  }
};

const logError = async (request: Request, response: Response) => {
  const contentType = response.headers.get("content-type");
  let body: any;
  if (contentType && contentType.indexOf("application/json") !== -1) {
    body = await response.json();
  } else {
    body = await response.text();
  }
  console.error(`Error reqesting ${request.method} ${request.url}`, body);
};

const logErrorBody = async (request: Request, body: any) => {
  console.error(`Error reqesting ${request.method} ${request.url}`, body);
};
