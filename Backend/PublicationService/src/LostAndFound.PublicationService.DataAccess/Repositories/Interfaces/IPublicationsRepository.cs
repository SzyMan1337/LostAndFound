using LostAndFound.PublicationService.DataAccess.Entities;
using LostAndFound.PublicationService.DataAccess.Models;
using MongoDB.Driver;

namespace LostAndFound.PublicationService.DataAccess.Repositories.Interfaces
{
    public interface IPublicationsRepository : IRepository<Publication>
    {
        Task UpdatePublicationPhotoUrl(Publication publicationEntity);
        Task UpdatePublicationState(Publication publicationEntity);
        Task DeletePublicationVote(Guid publicationId, Vote voteEntity);
        Task UpdatePublicationVote(Guid publicationId, Vote voteEntity);
        Task InsertNewPublicationVote(Guid publicationId, Vote voteEntity);
        Task<(long, IReadOnlyList<Publication>)> GetPublicationsPage(
            PublicationEntityPageParameters resourceParameters, Guid userId);
    }
}
