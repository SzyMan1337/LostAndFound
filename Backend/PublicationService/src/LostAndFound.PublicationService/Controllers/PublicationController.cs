using LostAndFound.PublicationService.Core.PublicationServices.Interfaces;
using LostAndFound.PublicationService.CoreLibrary.Internal;
using LostAndFound.PublicationService.CoreLibrary.Requests;
using LostAndFound.PublicationService.CoreLibrary.ResourceParameters;
using LostAndFound.PublicationService.CoreLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace LostAndFound.PublicationService.Controllers
{
    /// <summary>
    /// Publication controller responsible for posts data management
    /// </summary>
    [Route("[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationActionsService _publicationService;

        /// <summary>
        /// Default PublicationController constructor
        /// </summary>
        /// <param name="publicationService">Instance of IPublicationLostFoundService interface</param>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException when IPublicationLostFoundService is null</exception>
        public PublicationController(IPublicationActionsService publicationService)
        {
            _publicationService = publicationService ?? throw new ArgumentNullException(nameof(publicationService));
        }


        /// <summary>
        /// Get list of publication
        /// </summary>
        /// <param name="publicationsResourceParameters">Filter, search and pagination parameters to get publications</param>
        /// <response code="200">List of publication returned</response>
        /// <response code="401">Unauthorized access</response>
        /// <returns>List of publications</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /publication?orderBy=AggregateRating, IncidentDate desc
        ///
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Name = "GetPublications")]
        public async Task<ActionResult<IEnumerable<PublicationBaseDataResponseDto>>> GetPublications(
            [FromQuery] PublicationsResourceParameters publicationsResourceParameters)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var (publicationsDetailsDto, paginationMetadata) = await _publicationService
                .GetPublications(rawUserId, publicationsResourceParameters);

            paginationMetadata.NextPageLink = CreatePublicationsPageUri(
                publicationsResourceParameters, paginationMetadata, ResourceUriType.NextPage);
            paginationMetadata.PreviousPageLink = CreatePublicationsPageUri(
                publicationsResourceParameters, paginationMetadata, ResourceUriType.PreviousPage);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(publicationsDetailsDto);
        }

        /// <summary>
        /// Create Publication endpoint
        /// </summary>
        /// <param name="publicationData">Data of new publication</param>
        /// <response code="201">Publication created</response>
        /// <response code="401">Unauthorized access</response>
        /// <returns>New publication details</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /publication
        ///     {
        ///         "Title": "I lost my phone",
        ///         "Description": "On May 11, 2022, I lost my blue Iphone 11 pro.",
        ///         "IncidentAddress": "Ludwika Warynskiego 12, 00-655 Warszawa",
        ///         "IncidentDate": "2022-12-01T13:30:22.52Z",
        ///         "SubjectCategoryId": "Other",
        ///         "PublicationType": "LostSubject",
        ///         "SubjectPhoto": Photo       
        ///     }
        ///
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<PublicationDetailsResponseDto>> CreatePublication([FromForm] CreatePublicationRequestDto publicationData)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string username = HttpContext.User.FindFirstValue("username");

            var publicationDetails = await _publicationService.CreatePublication(rawUserId, username, publicationData);

            return CreatedAtRoute("GetPublicationDetails",
                 new
                 {
                     publicationId = publicationDetails.PublicationId,
                 },
                 publicationDetails);
        }

        /// <summary>
        /// Get publication details
        /// </summary>
        /// <param name="publicationId">Publication identifier</param>
        /// <response code="200">Publication details returned</response>
        /// <response code="401">Unauthorized access to publication</response>
        /// <response code="404">Publication <paramref name="publicationId"/> could not be found</response>
        /// <returns>Publication details</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /publication/2b1bafcd-b2fd-492b-b050-9b7027653716
        ///
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{publicationId:Guid}", Name = "GetPublicationDetails")]
        public async Task<ActionResult<PublicationDetailsResponseDto>> GetPublicationDetails(Guid publicationId)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var publicationDetails = await _publicationService.GetPublicationDetails(rawUserId, publicationId);

            return Ok(publicationDetails);
        }

        /// <summary>
        /// Update publication details
        /// </summary>
        /// <param name="publicationId">Publication identifier</param>
        /// <param name="publicationDetailsDto">Data to update publication details</param>
        /// <response code="200">Publication updated</response>
        /// <response code="401">Unauthorized access to publication</response>
        /// <response code="404">Publication <paramref name="publicationId"/> could not be found</response>
        /// <returns>Updated publication details</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /publication/2b1bafcd-b2fd-492b-b050-9b7027653716
        ///     {
        ///         "Title": "I lost my phone",
        ///         "Description": "On May 11, 2022, I lost my blue Iphone 11 pro.",
        ///         "IncidentAddress": "Ludwika Warynskiego 12, 00-655 Warszawa",
        ///         "IncidentDate": "2022-12-01T13:30:22.52Z",
        ///         "SubjectCategoryId": "Other",
        ///         "PublicationType": "LostSubject",
        ///         "PublicationState": "Open"
        ///     }
        ///
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{publicationId:Guid}")]
        public async Task<ActionResult<PublicationDetailsResponseDto>> UpdatePublicationDetails(Guid publicationId,
            UpdatePublicationDetailsRequestDto publicationDetailsDto)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var publicationDetails = await _publicationService
                .UpdatePublicationDetails(rawUserId, publicationId, publicationDetailsDto);

            return Ok(publicationDetails);
        }

        /// <summary>
        /// Update publication state
        /// </summary>
        /// <param name="publicationId">Publication identifier</param>
        /// <param name="publicationStateDto">Data to update publication state</param>
        /// <response code="200">Publication state updated</response>
        /// <response code="401">Unauthorized access to publication</response>
        /// <response code="404">Publication <paramref name="publicationId"/> could not be found</response>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /publication/2b1bafcd-b2fd-492b-b050-9b7027653716
        ///     {
        ///         "PublicationState": "Closed"
        ///     }
        ///     
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPatch("{publicationId:Guid}")]
        public async Task<ActionResult<PublicationDetailsResponseDto>> UpdatePublicationState(Guid publicationId,
            UpdatePublicationStateRequestDto publicationStateDto)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var publicationDetails = await _publicationService
                .UpdatePublicationState(rawUserId, publicationId, publicationStateDto);

            return Ok(publicationDetails);
        }

        /// <summary>
        /// Delete publication
        /// </summary>
        /// <param name="publicationId">Publication identifier</param>
        /// <response code="204">Publication deleted</response>
        /// <response code="401">Unauthorized access to publication</response>
        /// <response code="404">Publication <paramref name="publicationId"/> could not be found</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /publication/2b1bafcd-b2fd-492b-b050-9b7027653716
        ///
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{publicationId:Guid}")]
        public async Task<ActionResult> DeletePublication(Guid publicationId)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _publicationService.DeletePublication(rawUserId, publicationId);

            return NoContent();
        }

        /// <summary>
        /// Update user publication rating
        /// </summary>
        /// <param name="publicationId">Publication identifier</param>
        /// <param name="publicationRatingDto">Data to update user vote</param>
        /// <response code="200">Publication rating updated</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Publication <paramref name="publicationId"/> could not be found</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /publication/2b1bafcd-b2fd-492b-b050-9b7027653716/rating
        ///     {
        ///         "NewPublicationVote": Up
        ///     }
        ///
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPatch("{publicationId:Guid}/rating")]
        public async Task<ActionResult<PublicationDetailsResponseDto>> UpdatePublicationRating(Guid publicationId,
            UpdatePublicationRatingRequestDto publicationRatingDto)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var publicationDetails = await _publicationService
                .UpdatePublicationRating(rawUserId, publicationId, publicationRatingDto);

            return Ok(publicationDetails);
        }

        /// <summary>
        /// Update publication photo
        /// </summary>
        /// <param name="photo">Publication subject photo</param>
        /// <param name="publicationId">Publication identifier</param>
        /// <returns>User profile details</returns>
        /// <response code="200">Returns publication details</response>
        /// <response code="401">Problem with authentication of user occurred</response>
        /// <response code="404">Could not find publication <paramref name="publicationId"/></response>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /publication/2b1bafcd-b2fd-492b-b050-9b7027653716/photo
        ///
        /// </remarks>
        [HttpPatch("{publicationId:Guid}/photo")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicationDetailsResponseDto>> UpdatePublicationPhoto(
            Guid publicationId, IFormFile photo)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profileDetailsDto = await _publicationService.UpdatePublicationPhoto(photo, rawUserId, publicationId);

            return Ok(profileDetailsDto);
        }

        /// <summary>
        /// Delete publication photo
        /// </summary>
        /// <param name="publicationId">Publication identifier</param>
        /// <response code="204">Photo deleted</response>
        /// <response code="401">Problem with authentication of user occurred</response>
        /// <response code="404">Could not find publication <paramref name="publicationId"/></response>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /publication/2b1bafcd-b2fd-492b-b050-9b7027653716/photo
        ///
        /// </remarks>
        [HttpDelete("{publicationId:Guid}/photo")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeletePublicationPhoto(Guid publicationId)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _publicationService.DeletePublicationPhoto(rawUserId, publicationId);

            return NoContent();
        }

        private string? CreatePublicationsPageUri(PublicationsResourceParameters publicationsResourceParameters,
            PaginationMetadata paginationMetadata, ResourceUriType type)
        {
            double? searchRadius = publicationsResourceParameters.IncidentAddress is null ?
                null : publicationsResourceParameters.SearchRadius;

            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    if (paginationMetadata.CurrentPage <= 1)
                    {
                        return null;
                    }
                    return Url.Link("GetPublications",
                        new
                        {
                            pageNumber = paginationMetadata.CurrentPage - 1,
                            pageSize = paginationMetadata.PageSize,
                            onlyUserPublications = publicationsResourceParameters.OnlyUserPublications,
                            subjectCategoryId = publicationsResourceParameters.SubjectCategoryId,
                            publicationState = publicationsResourceParameters.PublicationState,
                            searchQuery = publicationsResourceParameters.SearchQuery,
                            incidentAddress = publicationsResourceParameters.IncidentAddress,
                            searchRadius = searchRadius,
                            fromDate = publicationsResourceParameters.FromDate,
                            toDate = publicationsResourceParameters.ToDate,
                            publicationType = publicationsResourceParameters.PublicationType,
                            orderBy = publicationsResourceParameters.OrderBy,
                        });
                case ResourceUriType.NextPage:
                    if (paginationMetadata.CurrentPage >= paginationMetadata.TotalPageCount)
                    {
                        return null;
                    }

                    return Url.Link("GetPublications",
                        new
                        {
                            pageNumber = paginationMetadata.CurrentPage + 1,
                            pageSize = paginationMetadata.PageSize,
                            onlyUserPublications = publicationsResourceParameters.OnlyUserPublications,
                            subjectCategoryId = publicationsResourceParameters.SubjectCategoryId,
                            publicationState = publicationsResourceParameters.PublicationState,
                            searchQuery = publicationsResourceParameters.SearchQuery,
                            incidentAddress = publicationsResourceParameters.IncidentAddress,
                            searchRadius = searchRadius,
                            fromDate = publicationsResourceParameters.FromDate,
                            toDate = publicationsResourceParameters.ToDate,
                            publicationType = publicationsResourceParameters.PublicationType,
                            orderBy = publicationsResourceParameters.OrderBy,
                        });
                default:
                    return Url.Link("GetPublications",
                        new
                        {
                            pageNumber = paginationMetadata.CurrentPage,
                            pageSize = paginationMetadata.PageSize,
                            onlyUserPublications = publicationsResourceParameters.OnlyUserPublications,
                            subjectCategoryId = publicationsResourceParameters.SubjectCategoryId,
                            publicationState = publicationsResourceParameters.PublicationState,
                            searchQuery = publicationsResourceParameters.SearchQuery,
                            incidentAddress = publicationsResourceParameters.IncidentAddress,
                            searchRadius = searchRadius,
                            fromDate = publicationsResourceParameters.FromDate,
                            toDate = publicationsResourceParameters.ToDate,
                            publicationType = publicationsResourceParameters.PublicationType,
                            orderBy = publicationsResourceParameters.OrderBy,
                        });
            }
        }
    }
}
