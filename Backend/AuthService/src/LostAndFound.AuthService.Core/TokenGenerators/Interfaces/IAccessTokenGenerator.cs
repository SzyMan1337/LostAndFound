using LostAndFound.AuthService.CoreLibrary.Internal;
using LostAndFound.AuthService.DataAccess.Entities;

namespace LostAndFound.AuthService.Core.TokenGenerators
{
    public interface IAccessTokenGenerator
    {
        AccessToken GenerateAccessToken(Account account);
    }
}
