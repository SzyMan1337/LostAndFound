namespace LostAndFound.AuthService.CoreLibrary.Requests
{
    /// <summary>
    /// Data to change account password
    /// </summary>
    public class ChangeAccountPasswordRequestDto
    {
        /// <summary>
        /// Current account password
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// New account password
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }
}
