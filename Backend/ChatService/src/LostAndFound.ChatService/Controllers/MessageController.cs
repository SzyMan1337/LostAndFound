using LostAndFound.ChatService.Core.MessageServices.Interfaces;
using LostAndFound.ChatService.CoreLibrary.Internal;
using LostAndFound.ChatService.CoreLibrary.Requests;
using LostAndFound.ChatService.CoreLibrary.ResourceParameters;
using LostAndFound.ChatService.CoreLibrary.Responses;
using LostAndFound.ChatService.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Text.Json;

namespace LostAndFound.ChatService.Controllers
{
    /// <summary>
    /// Message controller responsible for sending messages
    /// </summary>
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("chat/message")]
    [Produces("application/json")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hubContext;

        /// <summary>
        /// Default MessageController constructor
        /// </summary>
        /// <param name="messageService">Instance of IMessageService interface</param>
        /// <param name="hubContext">Chat hub context</param>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException when IMessageService is null</exception>
        public MessageController(IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }


        /// <summary>
        /// Send message to another user
        /// </summary>
        /// <param name="recipentId">Identifier of message recipent</param>
        /// <param name="messageRequestDto">Message data</param>
        /// <returns>Sent message details</returns>
        /// <response code="200">Message sent</response>
        /// <response code="204">Chat not found</response>
        /// <response code="401">Unauthorized access</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /message/2b1bafcd-b2fd-492b-b050-9b7027653716
        ///     {
        ///         "Content": "Co myslisz?"
        ///     }
        ///     
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("{recipentId:Guid}")]
        public async Task<ActionResult<MessageResponseDto>> SendMessage(Guid recipentId,
            CreateMessageRequestDto messageRequestDto)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var messageResponseDto = await _messageService
                .SendMessage(rawUserId, messageRequestDto, recipentId);

            await _hubContext.Clients.Group(recipentId.ToString())
                .SendAsync("ReceiveMessage", messageResponseDto);

            return Ok(messageResponseDto);
        }

        /// <summary>
        /// Get messages from chat
        /// </summary>
        /// <param name="recipentId">Message correspondent id</param>
        /// <param name="messagesResourceParameters">Pagination resource parameters</param>
        /// <returns>List of messages</returns>
        /// <response code="200">Messages list returned</response>
        /// <response code="204">Chat not found</response>
        /// <response code="401">Unauthorized access</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET chat/message/2b1bafcd-b2fd-492b-b050-9b7027653716?pageNumber=3
        ///
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{recipentId:Guid}", Name = "GetChatMessages")]
        public async Task<ActionResult<IEnumerable<MessageResponseDto>>> GetChatMessages(Guid recipentId,
            [FromQuery] MessagesResourceParameters messagesResourceParameters)
        {
            var rawUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var (messagesDtos, paginationMetadata) = await _messageService
                .GetChatMessages(rawUserId, messagesResourceParameters, recipentId);

            paginationMetadata.NextPageLink = CreateMessagesPageUri(paginationMetadata, ResourceUriType.NextPage);
            paginationMetadata.PreviousPageLink = CreateMessagesPageUri(paginationMetadata, ResourceUriType.PreviousPage);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(messagesDtos);
        }


        private string? CreateMessagesPageUri(PaginationMetadata paginationMetadata, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    if (paginationMetadata.CurrentPage <= 1)
                    {
                        return null;
                    }
                    return Url.Link("GetChatMessages",
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

                    return Url.Link("GetChatMessages",
                        new
                        {
                            pageNumber = paginationMetadata.CurrentPage + 1,
                            pageSize = paginationMetadata.PageSize
                        });
                default:
                    return Url.Link("GetChatMessages",
                        new
                        {
                            pageNumber = paginationMetadata.CurrentPage,
                            pageSize = paginationMetadata.PageSize
                        });
            }
        }
    }
}
