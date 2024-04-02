using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LostAndFound.PublicationService.DataAccess.Entities.Interfaces
{
    public interface IDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
