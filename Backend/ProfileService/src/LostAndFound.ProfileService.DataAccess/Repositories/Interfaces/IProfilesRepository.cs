using LostAndFound.ProfileService.DataAccess.Entities;

namespace LostAndFound.ProfileService.DataAccess.Repositories.Interfaces
{
    public interface IProfilesRepository : IRepository<Profile>
    {
        Task InsertNewProfileComment(Guid profileOwnerId, Comment commentEntity);
        Task UpdateProfileComment(Guid profileOwnerId, Comment commentEntity);
        Task DeleteProfileComment(Guid profileOwnerId, Comment commentEntity);
        Task UpdateProfilePictureUrl(Guid userId, string? pictureUrl);
    }
}
