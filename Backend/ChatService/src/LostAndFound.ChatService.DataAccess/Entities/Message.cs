namespace LostAndFound.ChatService.DataAccess.Entities
{
    public class Message
    {
        public string Content { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
