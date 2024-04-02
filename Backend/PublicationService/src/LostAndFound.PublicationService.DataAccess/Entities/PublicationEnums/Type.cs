using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LostAndFound.PublicationService.DataAccess.Entities.PublicationEnums
{
    public enum Type
    {
        [BsonRepresentation(BsonType.String)]
        LostSubject,
        [BsonRepresentation(BsonType.String)]
        FoundSubject,
    }
}
