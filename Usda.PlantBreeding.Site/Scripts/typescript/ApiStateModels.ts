interface Loading {
    kind: "loading";
}

interface Success<T> {
    kind: "success";
    content: T | null;
}

interface Failure {
    kind: "failure";
}

interface Reloading<T> {
    kind: "reloading";
    content: T | null;
}

export type Resource<T> = Loading | Success<T> | Reloading<T> | Failure


export function parseQueryString(url: string): { [key: string]: string[] | undefined } {
    let queryObject: { [key: string]: string[] | undefined } = {};
    const pairs = url.slice(url.indexOf("?") + 1).split("&");
    for (const pair of pairs) {
        const pairParts = pair.split("=");
        if (pairParts[0] && pairParts[1]) {
            queryObject[pairParts[0]] = pairParts[1].split(",");
        }
    }
    return queryObject;
}

export async function get<T>(url: string, params: object) {
    return new Promise<T>((resolve, reject) => {
        $.ajax({
            url: url,
            dataType: "json",
            traditional: false, 
            data: params,
            success: resolve,
            error: (xhr, status, err) => reject(new Error(err)),
            type: "GET"
        });
    });
}

export async function post<T>(url: string, params: object) {
    return new Promise<T>((resolve, reject) => {
        $.ajax({
            url: url,
            dataType: "json",
            traditional: false,
            data: params,
            success: resolve,
            error: (xhr, status, err) => reject(new Error(err)),
            type: "POST"
        });
    });
}