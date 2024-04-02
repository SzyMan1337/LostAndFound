namespace LostAndFound.ProfileService.CoreLibrary.ResourceParameters
{
    public class ProfileCommentsResourceParameters
    {
        private const int maxPageSize = 100;
        private int _pageSize = 20;

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
