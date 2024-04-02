using System;

namespace LostAndFound.AuthService.IntegrationTests.Models.Responses
{
    public class ExpectedAuthenticatedUserResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpirationTime { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
