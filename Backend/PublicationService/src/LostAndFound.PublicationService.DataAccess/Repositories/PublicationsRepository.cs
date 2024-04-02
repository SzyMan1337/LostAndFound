using LostAndFound.PublicationService.DataAccess.Context.Interfaces;
using LostAndFound.PublicationService.DataAccess.Entities;
using LostAndFound.PublicationService.DataAccess.Models;
using LostAndFound.PublicationService.DataAccess.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LostAndFound.PublicationService.DataAccess.Repositories
{
    public class PublicationsRepository : BaseRepository<Publication>, IPublicationsRepository
    {
        public PublicationsRepository(IMongoPublicationServiceDbContext publicationServiceDbContext) : base(publicationServiceDbContext) { }


        public async Task DeletePublicationVote(Guid publicationId, Vote voteEntity)
        {
            var filter = Builders<Publication>.Filter
                .Eq(publication => publication.ExposedId, publicationId);
            var update = Builders<Publication>.Update
                .Pull(publication => publication.Votes, voteEntity);

            await _collection.UpdateOneAsync(filter, update);

            await UpdatePublicationAggregateRating(publicationId);
        }

        public async Task InsertNewPublicationVote(Guid publicationId, Vote voteEntity)
        {
            var filter = Builders<Publication>.Filter
                .Eq(publication => publication.ExposedId, publicationId);
            var update = Builders<Publication>.Update
                .Push(publication => publication.Votes, voteEntity);

            await _collection.UpdateOneAsync(filter, update);

            await UpdatePublicationAggregateRating(publicationId);
        }

        public async Task UpdatePublicationPhotoUrl(Publication publicationEntity)
        {
            var filter = Builders<Publication>.Filter
                .Eq(publication => publication.ExposedId, publicationEntity.ExposedId);
            var update = Builders<Publication>.Update
                .Set(publication => publication.SubjectPhotoUrl, publicationEntity.SubjectPhotoUrl)
                .Set(publication => publication.LastModificationDate, publicationEntity.LastModificationDate);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdatePublicationState(Publication publicationEntity)
        {
            var filter = Builders<Publication>.Filter
                .Eq(publication => publication.ExposedId, publicationEntity.ExposedId);
            var update = Builders<Publication>.Update
                .Set(publication => publication.State, publicationEntity.State)
                .Set(publication => publication.LastModificationDate, publicationEntity.LastModificationDate);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdatePublicationVote(Guid publicationId, Vote voteEntity)
        {
            var filter = Builders<Publication>.Filter.Eq(publication => publication.ExposedId, publicationId)
                & Builders<Publication>.Filter.ElemMatch(p => p.Votes, Builders<Vote>.Filter.Eq(x => x.VoterId, voteEntity.VoterId));

            var update = Builders<Publication>.Update
                .Set(publication => publication.Votes[-1], voteEntity);

            await _collection.UpdateOneAsync(filter, update);

            await UpdatePublicationAggregateRating(publicationId);
        }

        public async Task<(long, IReadOnlyList<Publication>)> GetPublicationsPage(
            PublicationEntityPageParameters resourceParameters, Guid userId)
        {
            var filter = CreateFilterExpression(resourceParameters, userId);
            var sort = CreateSortExpression(resourceParameters.SortIndicator);

            return await AggregateByPage(filter, sort, resourceParameters.PageNumber, resourceParameters.PageSize);
        }

        private async Task UpdatePublicationAggregateRating(Guid publicationId)
        {
            var publicationEntity = await base
                .GetSingleAsync(publication => publication.ExposedId == publicationId);
            var newAggregateRating = publicationEntity.Votes.Any() ?
                publicationEntity.Votes.Sum(x => x.Rating) : 0;

            var filter = Builders<Publication>.Filter
                .Eq(publication => publication.ExposedId, publicationId);
            var update = Builders<Publication>.Update
                .Set(publication => publication.AggregateRating, newAggregateRating);

            await _collection.UpdateOneAsync(filter, update);
        }

        private static SortDefinition<Publication> CreateSortExpression(SortIndicatorData sortIndicator)
        {
            if (sortIndicator is null || !sortIndicator.IsSortDefined || !sortIndicator.PropertyIndications.Any())
            {
                return Builders<Publication>.Sort.Descending(p => p.AggregateRating);
            }

            var sortDefinitions = new List<SortDefinition<Publication>>();
            foreach (var ind in sortIndicator.PropertyIndications)
            {
                var sortDef = new BsonDocumentSortDefinition<Publication>(
                    new BsonDocument(ind.PropertyName, ind.SortType));

                sortDefinitions.Add(sortDef);
            }

            return Builders<Publication>.Sort.Combine(sortDefinitions);
        }

        private static FilterDefinition<Publication> CreateFilterExpression(
            PublicationEntityPageParameters resourceParameters, Guid userId)
        {
            var builder = Builders<Publication>.Filter;
            var filter = builder.Empty;

            if (resourceParameters.PublicationState is not null)
            {
                filter = builder.Eq(pub => pub.State, resourceParameters.PublicationState);
            }

            if (resourceParameters.PublicationType is not null)
            {
                filter &= builder.Eq(pub => pub.Type, resourceParameters.PublicationType);
            }

            if (resourceParameters.OnlyUserPublications)
            {
                filter &= builder.Eq(pub => pub.Author.Id, userId);
            }

            if (!String.IsNullOrEmpty(resourceParameters.SubjectCategoryId))
            {
                filter &= builder.Eq(pub => pub.SubjectCategoryId, resourceParameters.SubjectCategoryId);
            }

            if (resourceParameters.FromDate is not null)
            {
                filter &= builder.Gte(pub => pub.IncidentDate, resourceParameters.FromDate);
            }

            if (resourceParameters.ToDate is not null)
            {
                filter &= builder.Lte(pub => pub.IncidentDate, resourceParameters.ToDate);
            }

            if (!String.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                filter &= (builder.Regex(pub => pub.Description, new BsonRegularExpression($"/{resourceParameters.SearchQuery}/", "i")) |
                    builder.Regex(pub => pub.Title, new BsonRegularExpression($"/{resourceParameters.SearchQuery}/", "i")));
            }

            if (resourceParameters.CoordinateBoundaries is not null)
            {
                filter &= (builder.Gte(p => p.Latitude, resourceParameters.CoordinateBoundaries.MinLatitude)
                    & builder.Gte(p => p.Longitude, resourceParameters.CoordinateBoundaries.MinLongitude)
                    & builder.Lte(p => p.Latitude, resourceParameters.CoordinateBoundaries.MaxLatitude)
                    & builder.Lte(p => p.Longitude, resourceParameters.CoordinateBoundaries.MaxLatitude));
            }

            return filter;
        }
    }
}
