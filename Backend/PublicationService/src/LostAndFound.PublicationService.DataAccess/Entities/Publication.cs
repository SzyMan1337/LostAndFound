using LostAndFound.PublicationService.DataAccess.Attributes;
using PublicationState = LostAndFound.PublicationService.DataAccess.Entities.PublicationEnums.State;
using PublicationType = LostAndFound.PublicationService.DataAccess.Entities.PublicationEnums.Type;

namespace LostAndFound.PublicationService.DataAccess.Entities
{
    [BsonCollection("publications")]
    public class Publication : BaseDocument
    {
        public Guid ExposedId { get; set; }
        public Author Author { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? SubjectPhotoUrl { get; set; }
        public string IncidentAddress { get; set; } = string.Empty;
        public DateTime IncidentDate { get; set; }
        public string SubjectCategoryId { get; set; } = string.Empty;
        public string SubjectCategoryName { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public PublicationType Type { get; set; }
        public PublicationState State { get; set; }
        public long AggregateRating { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Vote[] Votes { get; set; } = Array.Empty<Vote>();
    }
}
