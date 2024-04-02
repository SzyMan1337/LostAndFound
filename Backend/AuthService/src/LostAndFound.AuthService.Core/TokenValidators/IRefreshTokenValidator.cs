namespace LostAndFound.AuthService.Core.TokenValidators
{
    public interface IRefreshTokenValidator
    {
        bool ValidateRefreshToken(string refreshToken);
    }
}
