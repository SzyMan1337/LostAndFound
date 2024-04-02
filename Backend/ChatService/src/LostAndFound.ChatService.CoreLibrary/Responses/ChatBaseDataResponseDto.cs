namespace LostAndFound.ChatService.CoreLibrary.Responses
{
    /// <summary>
    /// Base chat data 
    /// </summary>
    public class ChatBaseDataResponseDto
    {
        /// <summary>
        /// Chat identifier
        /// </summary>
        public Guid ChatId { get; set; }

        /// <summary>
        /// Flag to indicate if there is any unread message
        /// </summary>
        public bool ContainsUnreadMessage { get; set; }

        /// <summary>
        /// Last message from the chat
        /// </summary>
        public MessageResponseDto? LastMessage { get; set; }

        /// <summary>
        /// The user with whom the chat is conducted
        /// </summary>
        public ChatMemberBaseDataResponseDto ChatMember { get; set; } = null!;
    }
}
