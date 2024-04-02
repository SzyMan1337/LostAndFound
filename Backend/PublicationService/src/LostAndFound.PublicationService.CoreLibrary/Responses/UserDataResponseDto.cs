namespace LostAndFound.PublicationService.CoreLibrary.Responses
{
    /// <summary>
    /// Base user data
    /// </summary>
    public class UserDataResponseDto
    {
        /// <summary>
        /// Author Identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Author username
        /// </summary>
        public string Username { get; set; } = string.Empty;
    }
}
