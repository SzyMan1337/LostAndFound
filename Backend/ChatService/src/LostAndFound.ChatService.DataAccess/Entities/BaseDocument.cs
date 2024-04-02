using LostAndFound.ChatService.DataAccess.Entities.Interfaces;
using MongoDB.Bson;

namespace LostAndFound.ChatService.DataAccess.Entities
{
    public abstract class BaseDocument : IDocument
    {
        public ObjectId Id { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
