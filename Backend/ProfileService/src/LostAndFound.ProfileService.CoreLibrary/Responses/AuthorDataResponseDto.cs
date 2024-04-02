namespace LostAndFound.ProfileService.CoreLibrary.Responses
{
    /// <summary>
    /// Base comment author data
    /// </summary>
    public class AuthorDataResponseDto
    {
        /// <summary>
        /// Author Identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Author username
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Author profile picture Url
        /// </summary>
        public string? PictureUrl { get; set; }
    }
}