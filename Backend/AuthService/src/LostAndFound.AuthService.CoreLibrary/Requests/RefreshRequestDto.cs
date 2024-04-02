namespace LostAndFound.AuthService.CoreLibrary.Requests
{
    /// <summary>
    /// Dto used for refreshing access token
    /// </summary>
    public class RefreshRequestDto
    {
        /// <summary>
        /// Jwt token
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;
    }
}
