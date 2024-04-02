namespace LostAndFound.AuthService.CoreLibrary.Requests
{
    /// <summary>
    /// User data to sign in
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// User's email address
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User's password
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
