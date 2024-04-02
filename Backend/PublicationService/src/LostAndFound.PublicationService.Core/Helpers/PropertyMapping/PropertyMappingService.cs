using LostAndFound.PublicationService.Core.Helpers.PropertyMapping.Interfaces;
using LostAndFound.PublicationService.CoreLibrary.Exceptions;
using LostAndFound.PublicationService.CoreLibrary.Internal;
using LostAndFound.PublicationService.CoreLibrary.Internal.Interfaces;
using LostAndFound.PublicationService.CoreLibrary.Responses;
using LostAndFound.PublicationService.DataAccess.Entities;
using LostAndFound.PublicationService.DataAccess.Models;

namespace LostAndFound.PublicationService.Core.Helpers.PropertyMapping
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _publicationPropertyMapping =
            new(StringComparer.OrdinalIgnoreCase)
            {
                { "Title", new(new[] { "Title" }) },
                { "Description", new(new[] { "Description" }) },
                { "IncidentDate", new(new[] { "IncidentDate" }) },
                { "AggregateRating", new(new[] { "AggregateRating" }) },
                { "PublicationState", new(new[] { "State" }) },
                { "PublicationType", new(new[] { "Type" }) },
                { "SubjectCategoryId", new(new[] { "SubjectCategoryId" }) },
            };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<PublicationBaseDataResponseDto, Publication>(
                _publicationPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping
              <TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings
                .OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)},{typeof(TDestination)}");
        }

        public IEnumerable<PropertySortIndication> GetPropertySortIndications<TSource, TDestination>(string orderBy)
        {
            var propertyIndications = new List<PropertySortIndication>();

            if (!String.IsNullOrEmpty(orderBy))
            {
                var publicationPropertyMappingDictionary = GetPropertyMapping<TSource, TDestination>();
                if (publicationPropertyMappingDictionary is null)
                {
                    throw new ArgumentNullException(nameof(publicationPropertyMappingDictionary));
                }

                var orderByAfterSplit = orderBy.Split(',');
                foreach (var orderByClause in orderByAfterSplit)
                {
                    var trimmedOrderByClause = orderByClause.Trim();
                    var orderAscending = !trimmedOrderByClause.EndsWith(" desc");

                    var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
                    var propertyName = indexOfFirstSpace == -1 ?
                        trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

                    if (!publicationPropertyMappingDictionary.ContainsKey(propertyName))
                    {
                        throw new BadRequestException($"Requested sorting by field {propertyName} is not supported.");
                    }

                    var propertyMappingValue = publicationPropertyMappingDictionary[propertyName];
                    if (propertyMappingValue == null)
                    {
                        throw new ArgumentNullException(nameof(propertyMappingValue));
                    }
                    orderAscending = propertyMappingValue.Revert ? !orderAscending : orderAscending;

                    foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
                    {
                        propertyIndications.Add(new PropertySortIndication()
                        {
                            IsSortAscending = orderAscending,
                            PropertyName = destinationProperty,
                        });
                    }
                }
            }

            return propertyIndications;
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }
            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var trimmedField = field.Trim();

                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
