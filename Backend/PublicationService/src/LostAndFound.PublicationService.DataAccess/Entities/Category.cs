using LostAndFound.PublicationService.DataAccess.Attributes;

namespace LostAndFound.PublicationService.DataAccess.Entities
{
    [BsonCollection("categories")]
    public class Category : BaseDocument
    {
        public string ExposedId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
