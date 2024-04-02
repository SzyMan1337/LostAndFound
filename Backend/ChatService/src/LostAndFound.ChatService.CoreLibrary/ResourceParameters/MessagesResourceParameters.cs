namespace LostAndFound.ChatService.CoreLibrary.ResourceParameters
{
    /// <summary>
    /// Messages resource parameters
    /// </summary>
    public class MessagesResourceParameters
    {
        private const int maxMessagesPageSize = 100;
        private int _pageSize = 50;

        /// <summary>
        /// Page number
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxMessagesPageSize) ? maxMessagesPageSize : value;
        }
    }
}
