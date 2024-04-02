using LostAndFound.ProfileService.Core.UserProfileServices.Interfaces;
using LostAndFound.ProfileService.CoreLibrary.Requests;
using LostAndFound.ProfileService.CoreLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LostAndFound.ProfileService.Controllers
{
    /// <summary>
    /// Profile controller responsible for user profile data management
    /// </summary>
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        /// <summary>
        /// Default ProfileController constructor
        /// </summary>
        /// <param name="userProfileService">Instance of IUserProfileService interface</param>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException when IUserProfileService is null</exception>
        public ProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService ?? throw new ArgumentNullException(nameof(userProfileService));
        }


        /// <summary>
        /// Get authenticated user profile details
        /// </summary>
        /// <returns>User profile details</returns>
        /// <response code="200">Returns profile details</response>
        /// <response code="401">Problem with authentication of user occurred</response>
        /// <response code="404">Could not find profile corresponding to authenticated user</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /profile
        ///
        /// </remarks>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Name = "GetProfileDetails")]
        public async Task<ActionResult<ProfileDetailsResponseDto>> GetProfileDetails()
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profileDetailsDto = await _userProfileService.GetUserProfileDetails(rawUserId);

            return Ok(profileDetailsDto);
        }

        /// <summary>
        /// Update authenticated user profile details
        /// </summary>
        /// <param name="updateProfileDetailsRequestDto">Updated user profile details data</param>
        /// <returns>Updated user profile details</returns>
        /// <response code="200">Returns updated profile details</response>
        /// <response code="401">Problem with authentication of user occurred</response>
        /// <response code="404">Could not find profile corresponding to authenticated user</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /profile
        ///     {
        ///        "Name": "Piotr",
        ///        "Surname": "Kowalski",
        ///        "Description": "I like cats",
        ///        "City": "Warsaw",
        ///     }
        ///
        /// </remarks>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<ActionResult<ProfileDetailsResponseDto>> UpdateProfileDetails(UpdateProfileDetailsRequestDto updateProfileDetailsRequestDto)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profileDetailsDto = await _userProfileService.UpdateProfileDetails(updateProfileDetailsRequestDto, rawUserId);

            return Ok(profileDetailsDto);
        }

        /// <summary>
        /// Retrieve user profile details with identifier <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>User profile details</returns>
        /// <response code="200">Returns profile details</response>
        /// <response code="401">Problem with authentication of user occurred</response>
        /// <response code="404">Could not find profile corresponding to authenticated user</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /profile/2b1bafcd-b2fd-492b-b050-9b7027653716
        ///
        /// </remarks>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{userId}")]
        public async Task<ActionResult<ProfileDetailsResponseDto>> GetProfileDetailsOfUser(string userId)
        {
            var profileDetailsDto = await _userProfileService.GetUserProfileDetails(userId);

            return Ok(profileDetailsDto);
        }

        /// <summary>
        /// Update user profile picture
        /// </summary>
        /// <param name="picture">User profile picture</param>
        /// <returns>User profile details</returns>
        /// <response code="200">Returns profile details</response>
        /// <response code="401">Problem with authentication of user occurred</response>
        /// <response code="404">Could not find profile corresponding to authenticated user</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /profile/picture
        ///
        /// </remarks>
        [Authorize]
        [HttpPatch("picture")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProfileDetailsResponseDto>> UpdateUserProfilePicture(IFormFile picture)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profileDetailsDto = await _userProfileService.UpdateUserProfilePicture(picture, rawUserId);

            return Ok(profileDetailsDto);
        }

        /// <summary>
        /// Delete user profile picture
        /// </summary>
        /// <response code="204">Picture deleted</response>
        /// <response code="401">Problem with authentication of user occurred</response>
        /// <response code="404">Could not find profile corresponding to authenticated user</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /profile/picture
        ///
        /// </remarks>
        [Authorize]
        [HttpDelete("picture")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteUserProfilePicture()
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userProfileService.DeleteUserProfilePicture(rawUserId);

            return NoContent();
        }

        /// <summary>
        /// Retrieve users profile base data
        /// </summary>
        /// <param name="userIds">List of users id's</param>
        /// <returns>Users profile base data</returns>
        /// <response code="200">Returns profiles base data</response>
        /// <response code="401">Problem with authentication of user occurred</response>
        /// <response code="404">Could not find data for one of the user</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /profile/list
        ///     {
        ///         QUERY:
        ///             "UserIds": [
        ///                 "2b1bafcd-b2fd-492b-b050-9b7027653716",
        ///                 "4c1bafcd-b2fd-492b-b050-9c7027653712"
        ///             ]
        ///     }
        ///
        /// </remarks>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<ProfileBaseDataResponseDto>>> GetBaseProfileDataForListOfUsers(
            [FromQuery] IEnumerable<Guid> userIds)
        {
            var profilesBaseData = await _userProfileService.GetBaseProfileDataForListOfUsers(
                userIds);

            return Ok(profilesBaseData);
        }
    }
}
