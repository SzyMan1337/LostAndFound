namespace LostAndFound.AuthService.CoreLibrary.Messages
{
    /// <summary>
    /// Basic details of the new user
    /// </summary>
    public class NewUserAccountMessageDto
    {
        /// <summary>
        /// User Identifier
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The user e-mail
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The username
        /// </summary>
        public string Username { get; set; } = string.Empty;
    }
}
