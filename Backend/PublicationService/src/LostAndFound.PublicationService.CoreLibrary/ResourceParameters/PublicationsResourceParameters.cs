using LostAndFound.PublicationService.CoreLibrary.Enums;

namespace LostAndFound.PublicationService.CoreLibrary.ResourceParameters
{
    /// <summary>
    /// Publications resource parameters
    /// </summary>
    public class PublicationsResourceParameters
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

        /// <summary>
        /// Include only user publications
        /// </summary>
        public bool OnlyUserPublications { get; set; } = false;

        /// <summary>
        /// Filter by publication subject category identifier
        /// </summary>
        public string? SubjectCategoryId { get; set; }

        /// <summary>
        /// State of publication to filter
        /// </summary>
        public PublicationState? PublicationState { get; set; }

        /// <summary>
        /// Search for publications containing the given searchQuery in the title or description
        /// </summary>
        public string? SearchQuery { get; set; }

        /// <summary>
        /// Filter publications by distance from incident address and given address
        /// </summary>
        public string? IncidentAddress { get; set; }

        /// <summary>
        /// Search radius from the given address
        /// </summary>
        public double SearchRadius { get; set; } = 10d;

        /// <summary>
        /// Filter publications from accident date
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Filter publications to accident date
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Type of publication to filter
        /// </summary>
        public PublicationType? PublicationType { get; set; }

        /// <summary>
        /// Order by parameter
        /// </summary>
        /// <remarks>
        /// Sample orderBy value:
        ///
        ///     AggregateRating, IncidentDate desc
        ///
        /// </remarks>
        public string? OrderBy { get; set; }
    }
}
