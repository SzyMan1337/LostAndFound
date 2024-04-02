using LostAndFound.ChatService.Core.ChatServices.Interfaces;
using LostAndFound.ChatService.CoreLibrary.Internal;
using LostAndFound.ChatService.CoreLibrary.ResourceParameters;
using LostAndFound.ChatService.CoreLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace LostAndFound.ChatService.Controllers
{
    /// <summary>
    /// Chat controller responsible for managing correspondence history
    /// </summary>
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatActionService _chatActionService;


        /// <summary>
        /// Default ChatController constructor
        /// </summary>
        /// <param name="chatActionService">Instance of IChatActionService interface</param>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException when IChatActionService is null</exception>
        public ChatController(IChatActionService chatActionService)
        {
            _chatActionService = chatActionService ?? throw new ArgumentNullException(nameof(chatActionService));
        }


        /// <summary>
        /// Get list of user chats
        /// </summary>
        /// <param name="chatsResource">Pagination parameters to get list of chats</param>
        /// <response code="200">Chat list returned</response>
        /// <response code="401">Unauthorized access</response>
        /// <returns>List of user chats</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /chat?pageNumber=3
        ///
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Name = "GetChats")]
        public async Task<ActionResult<IEnumerable<ChatBaseDataResponseDto>>> GetChats(
            [FromQuery] ChatsResourceParameters chatsResource)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var (chatDtos, paginationMetadata) = await _chatActionService
                .GetChats(rawUserId, chatsResource);

            paginationMetadata.NextPageLink = CreateChatsPageUri(paginationMetadata, ResourceUriType.NextPage);
            paginationMetadata.PreviousPageLink = CreateChatsPageUri(paginationMetadata, ResourceUriType.PreviousPage);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(chatDtos);
        }

        /// <summary>
        /// Read all messages from chat
        /// </summary>
        /// <param name="chatMemberId">Chat member identifier</param>
        /// <response code="204">Chat message read</response>
        /// <response code="404">Chat not found</response>
        /// <response code="401">Unauthorized access</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /chat/2b1bafcd-b2fd-492b-b050-9b7027653716
        ///
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{chatMemberId:Guid}")]
        public async Task<ActionResult> ReadChatMessages(Guid chatMemberId)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _chatActionService.ReadChatMessages(rawUserId, chatMemberId);

            return NoContent();
        }

        /// <summary>
        /// Get unread chats counter
        /// </summary>
        /// <returns>Unread chats notification counter</returns>
        /// <remarks>
        /// <response code="200">Notification data returned</response>
        /// <response code="401">Unauthorized access</response>
        /// Sample request:
        ///
        ///     GET /chat/notification
        ///
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("notification")]
        public async Task<ActionResult<ChatNotificationResponseDto>> GetUnreadChatNotificationCounter()
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _chatActionService.GetUnreadChatNotification(rawUserId);

            return Ok(response);
        }


        private string? CreateChatsPageUri(PaginationMetadata paginationMetadata, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    if (paginationMetadata.CurrentPage <= 1)
                    {
                        return null;
                    }
                    return Url.Link("GetChats",
                        new
                        {
                            pageNumber = paginationMetadata.CurrentPage - 1,
                            pageSize = paginationMetadata.PageSize
                        });
                case ResourceUriType.NextPage:
                    if (paginationMetadata.CurrentPage >= paginationMetadata.TotalPageCount)
                    {
                        return null;
                    }

                    return Url.Link("GetChats",
                        new
                        {
                            pageNumber = paginationMetadata.CurrentPage + 1,
                            pageSize = paginationMetadata.PageSize
                        });
                default:
                    return Url.Link("GetChats",
                        new
                        {
                            pageNumber = paginationMetadata.CurrentPage,
                            pageSize = paginationMetadata.PageSize
                        });
            }
        }
    }
}
