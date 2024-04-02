export var SinglePublicationVote;
(function (SinglePublicationVote) {
    SinglePublicationVote["NoVote"] = "NoVote";
    SinglePublicationVote["Up"] = "Up";
    SinglePublicationVote["Down"] = "Down";
})(SinglePublicationVote || (SinglePublicationVote = {}));
export var PublicationType;
(function (PublicationType) {
    PublicationType["LostSubject"] = "LostSubject";
    PublicationType["FoundSubject"] = "FoundSubject";
})(PublicationType || (PublicationType = {}));
export var PublicationState;
(function (PublicationState) {
    PublicationState["Open"] = "Open";
    PublicationState["Closed"] = "Closed";
})(PublicationState || (PublicationState = {}));
export const mapPublicationFromServer = (publication) => ({
    ...publication,
    incidentDate: new Date(publication.incidentDate),
    lastModificationDate: new Date(publication.lastModificationDate),
    creationDate: new Date(publication.creationDate),
});
export var Order;
(function (Order) {
    Order["Ascending"] = "Ascending";
    Order["Descending"] = "Descending";
})(Order || (Order = {}));
