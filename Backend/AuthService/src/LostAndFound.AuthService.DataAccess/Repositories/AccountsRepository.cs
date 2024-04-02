using LostAndFound.AuthService.DataAccess.Context.Interfaces;
using LostAndFound.AuthService.DataAccess.Entities;
using LostAndFound.AuthService.DataAccess.Repositories.Interfaces;
using MongoDB.Driver;

namespace LostAndFound.AuthService.DataAccess.Repositories
{
    public class AccountsRepository : BaseRepository<Account>, IAccountsRepository
    {
        public AccountsRepository(IMongoAuthServiceDbContext authServiceDbContext) : base(authServiceDbContext) { }

        public bool IsEmailInUse(string email)
        {
            var filter = Builders<Account>.Filter.Eq(acc => acc.Email, email);

            return _collection.FindSync(filter).Any();
        }

        public bool IsUsernameInUse(string username)
        {
            var filter = Builders<Account>.Filter.Eq(acc => acc.Username, username);

            return _collection.FindSync(filter).Any();
        }

        public async Task RemoveRefreshTokenFromAccountAsync(string email)
        {
            var filter = Builders<Account>.Filter.Eq(acc => acc.Email, email);
            var update = Builders<Account>.Update.Set(acc => acc.RefreshToken, null);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateAccountRefreshTokenAsync(string email, string refreshTokenRaw)
        {
            var filter = Builders<Account>.Filter.Eq(acc => acc.Email, email);
            var update = Builders<Account>.Update.Set(acc => acc.RefreshToken, refreshTokenRaw);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateAccountPasswordHashAsync(string email, string passwordHash)
        {
            var filter = Builders<Account>.Filter.Eq(acc => acc.Email, email);
            var update = Builders<Account>.Update.Set(acc => acc.PasswordHash, passwordHash);

            await _collection.UpdateOneAsync(filter, update);
        }
    }
}
