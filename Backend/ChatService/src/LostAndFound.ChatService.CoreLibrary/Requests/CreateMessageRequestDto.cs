namespace LostAndFound.ChatService.CoreLibrary.Requests
{
    /// <summary>
    /// Data for messsage creation
    /// </summary>
    public class CreateMessageRequestDto
    {
        /// <summary>
        /// Content of a message
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}
