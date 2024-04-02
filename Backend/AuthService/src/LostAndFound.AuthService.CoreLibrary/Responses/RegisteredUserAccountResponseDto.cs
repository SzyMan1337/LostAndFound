namespace LostAndFound.AuthService.CoreLibrary.Responses
{
    /// <summary>
    /// Data of newly created user's account
    /// </summary>
    public class RegisteredUserAccountResponseDto
    {
        /// <summary>
        /// User identifier
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Email address of new user account
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Username assigned to newly created account
        /// </summary>
        public string Username { get; set; } = string.Empty;
    }
}
