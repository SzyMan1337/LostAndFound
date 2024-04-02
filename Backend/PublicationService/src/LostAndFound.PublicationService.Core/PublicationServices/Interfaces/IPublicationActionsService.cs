using LostAndFound.PublicationService.CoreLibrary.Internal;
using LostAndFound.PublicationService.CoreLibrary.Requests;
using LostAndFound.PublicationService.CoreLibrary.ResourceParameters;
using LostAndFound.PublicationService.CoreLibrary.Responses;
using Microsoft.AspNetCore.Http;

namespace LostAndFound.PublicationService.Core.PublicationServices.Interfaces
{
    public interface IPublicationActionsService
    {
        Task DeletePublicationPhoto(string rawUserId, Guid publicationId);
        Task<PublicationDetailsResponseDto> UpdatePublicationPhoto(IFormFile photo, string rawUserId, Guid publicationId);
        Task<PublicationDetailsResponseDto> CreatePublication(string rawUserId, string username, 
            CreatePublicationRequestDto publicationData);
        Task<PublicationDetailsResponseDto> GetPublicationDetails(string rawUserId, Guid publicationId);
        Task<PublicationDetailsResponseDto> UpdatePublicationDetails(string rawUserId, Guid publicationId, 
            UpdatePublicationDetailsRequestDto publicationDetailsDto);
        Task DeletePublication(string rawUserId, Guid publicationId);
        Task<(IEnumerable<PublicationBaseDataResponseDto>?, PaginationMetadata)> GetPublications(string rawUserId,
            PublicationsResourceParameters publicationsResourceParameters);
        Task<PublicationDetailsResponseDto> UpdatePublicationState(string rawUserId, Guid publicationId, 
            UpdatePublicationStateRequestDto publicationStateDto);
        Task<PublicationDetailsResponseDto> UpdatePublicationRating(string rawUserId, Guid publicationId, 
            UpdatePublicationRatingRequestDto publicationRatingDto);
    }
}
