export const mapLoginFromServer = (data) => ({
    ...data,
    accessTokenExpirationTime: new Date(data.accessTokenExpirationTime),
});
