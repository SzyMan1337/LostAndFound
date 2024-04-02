using LostAndFound.ProfileService.CoreLibrary.Messages;
using LostAndFound.ProfileService.CoreLibrary.Requests;
using LostAndFound.ProfileService.CoreLibrary.Responses;
using Microsoft.AspNetCore.Http;

namespace LostAndFound.ProfileService.Core.UserProfileServices.Interfaces
{
    public interface IUserProfileService
    {
        Task CreateUserProfile(NewUserAccountMessageDto createProfileRequestDto);
        Task<ProfileDetailsResponseDto> GetUserProfileDetails(string rawUserId);
        Task<ProfileDetailsResponseDto> UpdateProfileDetails(UpdateProfileDetailsRequestDto updateProfileDetailsRequestDto, string rawUserId);
        Task<ProfileDetailsResponseDto> UpdateUserProfilePicture(IFormFile picture, string rawUserId);
        Task DeleteUserProfilePicture(string rawUserId);
        Task<IEnumerable<ProfileBaseDataResponseDto>> GetBaseProfileDataForListOfUsers(
             IEnumerable<Guid> guids);
    }
}
