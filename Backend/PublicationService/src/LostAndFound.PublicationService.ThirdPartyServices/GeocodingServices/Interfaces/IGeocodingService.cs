using LostAndFound.PublicationService.ThirdPartyServices.Models;

namespace LostAndFound.PublicationService.ThirdPartyServices.GeocodingServices.Interfaces
{
    public interface IGeocodingService
    {
        Task<AddressDataDto?> GeocodeAddress(string address);
    }
}
