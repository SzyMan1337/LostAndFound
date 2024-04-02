using LostAndFound.AuthService.CoreLibrary.Requests;
using LostAndFound.AuthService.CoreLibrary.Responses;

namespace LostAndFound.AuthService.Core.AccountServices
{
    public interface IAccountService
    {
        Task<RegisteredUserAccountResponseDto> RegisterUserAccount(RegisterUserAccountRequestDto dto);
        Task<AuthenticatedUserResponseDto> AuthenticateUser(LoginRequestDto dto);
        Task<AuthenticatedUserResponseDto> RefreshUserAuthentication(RefreshRequestDto dto);
        Task LogoutUser(string userId);
        Task ChangePassword(ChangeAccountPasswordRequestDto dto, string rawUserId);
    }
}
