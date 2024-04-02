namespace LostAndFound.PublicationService.ThirdPartyServices.Models
{
    public class GeocodedAddressesResponseDto
    {
        public IEnumerable<AddressDataDto> Data { get; set; } = new List<AddressDataDto>();
    }
}
