using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LostAndFound.PublicationService.DataAccess.Entities.PublicationEnums
{
    public enum State
    {
        [BsonRepresentation(BsonType.String)]
        Open,
        [BsonRepresentation(BsonType.String)]
        Closed,
    }
}
