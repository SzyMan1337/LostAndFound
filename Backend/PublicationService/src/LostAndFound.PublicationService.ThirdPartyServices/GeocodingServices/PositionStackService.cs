using LostAndFound.PublicationService.ThirdPartyServices.GeocodingServices.Interfaces;
using LostAndFound.PublicationService.ThirdPartyServices.Models;
using LostAndFound.PublicationService.ThirdPartyServices.Settings;
using Marvin.StreamExtensions;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;

namespace LostAndFound.PublicationService.ThirdPartyServices.GeocodingServices
{
    public class PositionStackService : IGeocodingService
    {
        private readonly HttpClient _client;
        private readonly PositionStackServiceSettings _settings;

        public PositionStackService(HttpClient client, PositionStackServiceSettings settings)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AddressDataDto?> GeocodeAddress(string address)
        {
            var queryParams = new Dictionary<string, string?>()
            {
                { "country", "PL" },
                { "limit", "1" },
                { "fields", "results.latitude,results.longitude" },
                { "query", address },
                { "access_key", _settings.AccessKey }
            };

            var requestUri = QueryHelpers.AddQueryString("forward", queryParams);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            using var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead);
            var stream = await response.Content.ReadAsStreamAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseDto = stream.ReadAndDeserializeFromJson<GeocodedAddressesResponseDto>();
            return responseDto?.Data?.FirstOrDefault();
        }
    }
}
