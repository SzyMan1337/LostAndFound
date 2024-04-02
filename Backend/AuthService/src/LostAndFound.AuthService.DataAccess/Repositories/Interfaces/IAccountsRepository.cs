using LostAndFound.AuthService.DataAccess.Entities;

namespace LostAndFound.AuthService.DataAccess.Repositories.Interfaces
{
    public interface IAccountsRepository : IRepository<Account>
    {
        bool IsEmailInUse(string value);
        bool IsUsernameInUse(string value);
        Task RemoveRefreshTokenFromAccountAsync(string email);
        Task UpdateAccountRefreshTokenAsync(string email, string refreshTokenRaw);
        Task UpdateAccountPasswordHashAsync(string email, string passwordHash);
    }
}
