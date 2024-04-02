using LostAndFound.ChatService.DataAccess.Attributes;

namespace LostAndFound.ChatService.DataAccess.Entities
{
    [BsonCollection("chats")]
    public class Chat : BaseDocument
    {
        public Guid ExposedId { get; set; }
        public Member[] Members { get; set; } = Array.Empty<Member>();
        public bool ContainUnreadMessage { get; set; }
        public Message[] Messages { get; set; } = Array.Empty<Message>();
    }
}
