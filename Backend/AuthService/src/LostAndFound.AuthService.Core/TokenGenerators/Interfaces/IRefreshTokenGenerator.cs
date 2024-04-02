namespace LostAndFound.AuthService.Core.TokenGenerators
{
    public interface IRefreshTokenGenerator
    {
        string GenerateRefreshToken();
    }
}
