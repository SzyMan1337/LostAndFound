import { webAPIUrl } from "./AppSettings";
export const http = async (config) => {
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
        }
        catch {
            return { ok: response.ok };
        }
    }
    else {
        try {
            const body = await response.json();
            logErrorBody(request, body);
            return { ok: response.ok, errors: body.errors };
        }
        catch {
            logError(request, response);
            return { ok: response.ok };
        }
    }
};
export const multipartFormDataHttp = async (config, requestData) => {
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
        }
        catch {
            return { ok: response.ok };
        }
    }
    else {
        try {
            const body = await response.json();
            logErrorBody(request, body);
            return { ok: response.ok, errors: body.errors };
        }
        catch {
            logError(request, response);
            return { ok: response.ok };
        }
    }
};
const logError = async (request, response) => {
    const contentType = response.headers.get("content-type");
    let body;
    if (contentType && contentType.indexOf("application/json") !== -1) {
        body = await response.json();
    }
    else {
        body = await response.text();
    }
    console.error(`Error reqesting ${request.method} ${request.url}`, body);
};
const logErrorBody = async (request, body) => {
    console.error(`Error reqesting ${request.method} ${request.url}`, body);
};
