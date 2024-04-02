namespace LostAndFound.PublicationService.DataAccess.Models
{
    public class SortIndicatorData
    {
        public IEnumerable<PropertySortIndication> PropertyIndications { get; private set; } = Enumerable.Empty<PropertySortIndication>();
        public bool IsSortDefined { get; private set; } = false;

        public static SortIndicatorData Empty => new SortIndicatorData();


        public SortIndicatorData(IEnumerable<PropertySortIndication>? orderPropertyIndications = null)
        {
            if (orderPropertyIndications is not null && orderPropertyIndications.Any())
            {
                IsSortDefined = true;
                PropertyIndications = orderPropertyIndications;
            }
        }
    }
}
