namespace LostAndFound.ChatService.CoreLibrary.Responses
{
    /// <summary>
    /// Chat message data
    /// </summary>
    public class MessageResponseDto
    {
        /// <summary>
        /// Conent of the message
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Message creation time
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Message author identifier
        /// </summary>
        public Guid AuthorId { get; set; }
    }
}
