namespace LostAndFound.ProfileService.DataAccess.Entities
{
    public class Comment
    {
        public string Content { get; set; } = string.Empty;
        public float Rating { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorUsername { get; set; } = string.Empty;
    }
}
