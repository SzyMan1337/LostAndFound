namespace LostAndFound.AuthService.CoreLibrary.Responses
{
    /// <summary>
    /// Newly generated authentication data
    /// </summary>
    public class AuthenticatedUserResponseDto
    {
        /// <summary>
        /// Valid newly generated Jwt token
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Expiration date of access token
        /// </summary>
        public DateTime AccessTokenExpirationTime { get; set; }

        /// <summary>
        /// Valid newly generated Jwt token
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;
    }
}
