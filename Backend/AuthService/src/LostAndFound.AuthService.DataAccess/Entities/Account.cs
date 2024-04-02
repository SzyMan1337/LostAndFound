using LostAndFound.AuthService.DataAccess.Attributes;

namespace LostAndFound.AuthService.DataAccess.Entities
{
    [BsonCollection("accounts")]
    public class Account : BaseDocument
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
    }
}
