namespace LostAndFound.ProfileService.CoreLibrary.Responses
{
    /// <summary>
    /// User profile base data
    /// </summary>
    public class ProfileBaseDataResponseDto
    {
        /// <summary>
        /// Profile owner ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Username of the profile owner
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// User profile picture Url
        /// </summary>
        public string? PictureUrl { get; set; }
    }
}
