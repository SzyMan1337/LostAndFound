namespace LostAndFound.ProfileService.CoreLibrary.Responses
{
    /// <summary>
    /// User profile details
    /// </summary>
    public class ProfileDetailsResponseDto
    {
        /// <summary>
        /// Profile owner ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// User's email address
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Username of the profile owner
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Name of the profile owner
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Surname of the profile owner
        /// </summary>
        public string? Surname { get; set; }

        /// <summary>
        /// Profile description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// City of residence of the profile owner
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// User profile picture Url
        /// </summary>
        public string? PictureUrl { get; set; }

        /// <summary>
        /// Average profile rating
        /// </summary>
        public float AverageProfileRating { get; set; }
    }
}
