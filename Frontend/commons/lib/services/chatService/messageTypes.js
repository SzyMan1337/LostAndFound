export const mapMessageFromServer = (message) => ({
    ...message,
    creationTime: new Date(message.creationTime),
});
