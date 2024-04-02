export const mapProfileCommentFromServer = (comment) => ({
    ...comment,
    creationDate: new Date(comment === null || comment === void 0 ? void 0 : comment.creationDate),
});
export const mapProfileCommentsSectionFromServer = (data) => {
    var _a;
    return ({
        myComment: data.myComment
            ? mapProfileCommentFromServer(data.myComment)
            : undefined,
        comments: (_a = data.comments) === null || _a === void 0 ? void 0 : _a.map(mapProfileCommentFromServer),
    });
};
