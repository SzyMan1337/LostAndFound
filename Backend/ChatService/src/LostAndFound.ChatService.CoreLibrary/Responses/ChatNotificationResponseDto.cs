namespace LostAndFound.ChatService.CoreLibrary.Responses
{
    /// <summary>
    /// Chat notifications data
    /// </summary>
    public class ChatNotificationResponseDto
    {
        /// <summary>
        /// Number of chats with unread messages
        /// </summary>
        public int UnreadChatsCount { get; set; }

        /// <summary>
        /// Senders of unread messages data
        /// </summary>
        public IEnumerable<ChatMemberBaseDataResponseDto> UnreadMessageSenders { get; set; } 
            = Enumerable.Empty<ChatMemberBaseDataResponseDto>();
    }
}
