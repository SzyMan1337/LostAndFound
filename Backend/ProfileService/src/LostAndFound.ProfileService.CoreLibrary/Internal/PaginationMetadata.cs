namespace LostAndFound.ProfileService.CoreLibrary.Internal
{
    public class PaginationMetadata
    {
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string? NextPageLink { get; set; }
        public string? PreviousPageLink { get; set; }

        public PaginationMetadata(int totalItemCount, int pageSize, int currentPage)
        {
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            PageSize = pageSize;
            CurrentPage = currentPage;
        }
    }
}
