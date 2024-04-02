namespace LostAndFound.AuthService.IntegrationTests.Models.Requests
{
    public class ExpectedRegisterUserAccountRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
