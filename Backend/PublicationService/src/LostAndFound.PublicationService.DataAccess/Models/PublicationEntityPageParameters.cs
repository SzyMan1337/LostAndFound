namespace LostAndFound.PublicationService.DataAccess.Models
{
    public class PublicationEntityPageParameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool OnlyUserPublications { get; set; } = false;
        public string? SubjectCategoryId { get; set; }
        public Entities.PublicationEnums.State? PublicationState { get; set; }
        public string? SearchQuery { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Entities.PublicationEnums.Type? PublicationType { get; set; }
        public CoordinateLocationBoundaries? CoordinateBoundaries { get; set; }
        public SortIndicatorData SortIndicator { get; set; } = SortIndicatorData.Empty;
    }
}
