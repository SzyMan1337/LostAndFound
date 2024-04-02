using LostAndFound.ProfileService.DataAccess.Context.Interfaces;
using LostAndFound.ProfileService.DataAccess.Entities;
using LostAndFound.ProfileService.DataAccess.Repositories.Interfaces;
using MongoDB.Driver;

namespace LostAndFound.ProfileService.DataAccess.Repositories
{
    public class ProfilesRepository : BaseRepository<Profile>, IProfilesRepository
    {
        public ProfilesRepository(IMongoProfileServiceDbContext profileServiceDbContext) : base(profileServiceDbContext) { }

        public async Task DeleteProfileComment(Guid profileOwnerId, Comment commentEntity)
        {
            var filter = Builders<Profile>.Filter.Eq(profile => profile.UserId, profileOwnerId);
            var update = Builders<Profile>.Update.Pull(profile => profile.Comments, commentEntity);

            await _collection.UpdateOneAsync(filter, update);

            await UpdateAverageProfileRating(profileOwnerId);
        }

        public async Task InsertNewProfileComment(Guid profileOwnerId, Comment commentEntity)
        {
            var filter = Builders<Profile>.Filter.Eq(profile => profile.UserId, profileOwnerId);
            var update = Builders<Profile>.Update.Push(profile => profile.Comments, commentEntity);

            await _collection.UpdateOneAsync(filter, update);

            await UpdateAverageProfileRating(profileOwnerId);
        }

        public async Task UpdateProfileComment(Guid profileOwnerId, Comment commentEntity)
        {
            var filter = Builders<Profile>.Filter.Eq(p => p.UserId, profileOwnerId)
                & Builders<Profile>.Filter.ElemMatch(p => p.Comments, Builders<Comment>.Filter.Eq(x => x.AuthorId, commentEntity.AuthorId));
            var update = Builders<Profile>.Update.Set(profile => profile.Comments[-1], commentEntity);

            await _collection.UpdateOneAsync(filter, update);

            await UpdateAverageProfileRating(profileOwnerId);
        }

        public async Task UpdateProfilePictureUrl(Guid profileOwnerId, string? pictureUrl)
        {
            var filter = Builders<Profile>.Filter.Eq(profile => profile.UserId, profileOwnerId);
            var update = Builders<Profile>.Update.Set(profile => profile.PictureUrl, pictureUrl);

            await _collection.UpdateOneAsync(filter, update);
        }

        private async Task UpdateAverageProfileRating(Guid profileOwnerId)
        {
            var profileEntity = await base.GetSingleAsync(prof => prof.UserId == profileOwnerId);
            var newProfileRating = profileEntity.Comments.Any() ?
                profileEntity.Comments.Average(x => x.Rating) : 0;

            var filter = Builders<Profile>.Filter.Eq(profile => profile.UserId, profileOwnerId);
            var update = Builders<Profile>.Update.Set(profile => profile.AverageRating, newProfileRating);

            await _collection.UpdateOneAsync(filter, update);
        }
    }
}
