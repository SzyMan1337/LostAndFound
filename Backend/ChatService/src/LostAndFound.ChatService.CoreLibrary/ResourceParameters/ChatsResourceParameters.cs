namespace LostAndFound.ChatService.CoreLibrary.ResourceParameters
{
    /// <summary>
    /// Chats resource parameters
    /// </summary>
    public class ChatsResourceParameters
    {
        private const int maxPageSize = 100;
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
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
