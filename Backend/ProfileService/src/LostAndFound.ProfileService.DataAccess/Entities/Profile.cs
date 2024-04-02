using LostAndFound.ProfileService.DataAccess.Attributes;

namespace LostAndFound.ProfileService.DataAccess.Entities
{
    [BsonCollection("profiles")]
    public class Profile : BaseDocument
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }
        public string? PictureUrl { get; set; }
        public float AverageRating { get; set; }
        public Comment[] Comments { get; set; } = Array.Empty<Comment>();
    }
}
