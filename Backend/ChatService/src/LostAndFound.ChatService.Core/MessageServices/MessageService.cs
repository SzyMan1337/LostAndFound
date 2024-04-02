using AutoMapper;
using LostAndFound.ChatService.Core.DateTimeProviders;
using LostAndFound.ChatService.Core.Extensions;
using LostAndFound.ChatService.Core.MessageServices.Interfaces;
using LostAndFound.ChatService.CoreLibrary.Exceptions;
using LostAndFound.ChatService.CoreLibrary.Internal;
using LostAndFound.ChatService.CoreLibrary.Requests;
using LostAndFound.ChatService.CoreLibrary.ResourceParameters;
using LostAndFound.ChatService.CoreLibrary.Responses;
using LostAndFound.ChatService.DataAccess.Entities;
using LostAndFound.ChatService.DataAccess.Repositories.Interfaces;

namespace LostAndFound.ChatService.Core.MessageServices
{
    public class MessageService : IMessageService
    {
        private readonly IChatsRepository _chatsRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;

        public MessageService(IChatsRepository chatsRepository,
            IDateTimeProvider dateTimeProvider, IMapper mapper)
        {
            _chatsRepository = chatsRepository ?? throw new NotImplementedException(nameof(chatsRepository));
            _dateTimeProvider = dateTimeProvider ?? throw new NotImplementedException(nameof(dateTimeProvider));
            _mapper = mapper ?? throw new NotImplementedException(nameof(mapper));
        }

        public async Task<(IEnumerable<MessageResponseDto>?, PaginationMetadata)> GetChatMessages(string rawUserId,
            MessagesResourceParameters messagesResourceParameters, Guid recipentId)
        {
            var userId = ParseUserId(rawUserId);
            var chatId = userId.MungeTwoGuids(recipentId);
            var chatEntity = await _chatsRepository.GetSingleAsync(c => c.ExposedId == chatId);

            if (chatEntity?.Messages is null)
            {
                throw new NotFoundException("Chat not found");
            }

            var messagesPage = chatEntity.Messages
                .OrderByDescending(m => m.CreationTime)
                .Skip(messagesResourceParameters.PageSize * (messagesResourceParameters.PageNumber - 1))
                .Take(messagesResourceParameters.PageSize)
                .ToList();

            var messagesDtos = Enumerable.Empty<MessageResponseDto>();
            if (messagesPage is not null && messagesPage.Any())
            {
                messagesDtos = _mapper.Map<IEnumerable<MessageResponseDto>>(messagesPage);
            }

            int totalItemCount = chatEntity.Messages.Length;
            var paginationMetadata = new PaginationMetadata(totalItemCount,
                messagesResourceParameters.PageSize, messagesResourceParameters.PageNumber);

            return (messagesDtos, paginationMetadata);
        }

        public async Task<MessageResponseDto> SendMessage(string rawUserId,
            CreateMessageRequestDto messageRequestDto, Guid recipentId)
        {
            var userId = ParseUserId(rawUserId);
            if (userId == recipentId)
            {
                throw new BadRequestException("You cannot send messages to yourself");
            }

            var chatId = userId.MungeTwoGuids(recipentId);
            var chatEntity = await _chatsRepository.GetSingleAsync(c => c.ExposedId == chatId);

            var messageEntity = _mapper.Map<Message>(messageRequestDto);
            messageEntity.AuthorId = userId;
            messageEntity.CreationTime = _dateTimeProvider.UtcNow;

            if (chatEntity is null)
            {
                Chat newChat = CreateChatEntity(recipentId, userId, chatId, messageEntity);
                await _chatsRepository.InsertOneAsync(newChat);
            }
            else
            {
                await _chatsRepository.InsertNewChatMessage(chatId, messageEntity);
            }

            return _mapper.Map<MessageResponseDto>(messageEntity);
        }

        private Chat CreateChatEntity(Guid recipentId, Guid userId, Guid chatId, Message messageEntity)
        {
            return new Chat()
            {
                ExposedId = chatId,
                ContainUnreadMessage = true,
                CreationTime = _dateTimeProvider.UtcNow,
                Members = new Member[2]
                {
                        new Member() { Id = userId },
                        new Member() { Id = recipentId }
                },
                Messages = new Message[1] { messageEntity }
            };
        }

        private static Guid ParseUserId(string rawUserId)
        {
            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                throw new UnauthorizedException();
            }

            return userId;
        }
    }
}
