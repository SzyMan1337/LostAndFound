using LostAndFound.PublicationService.CoreLibrary.Internal;
using LostAndFound.PublicationService.DataAccess.Models;

namespace LostAndFound.PublicationService.Core.Helpers.PropertyMapping.Interfaces
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        bool ValidMappingExistsFor<TSource, TDestination>(string fields);
        IEnumerable<PropertySortIndication> GetPropertySortIndications<TSource, TDestination>(string fields);
    }
}
