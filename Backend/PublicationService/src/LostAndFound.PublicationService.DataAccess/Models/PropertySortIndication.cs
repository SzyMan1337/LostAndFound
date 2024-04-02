namespace LostAndFound.PublicationService.DataAccess.Models
{
    public class PropertySortIndication
    {
        public string PropertyName { get; set; } = string.Empty;
        public bool IsSortAscending { get; set; } = true;

        public int SortType => IsSortAscending ? 1 : -1;
    }
}
