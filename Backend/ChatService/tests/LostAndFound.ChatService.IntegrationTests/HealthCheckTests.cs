using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LostAndFound.ChatService.IntegrationTests
{
    public class HealthCheckTests : IClassFixture<IntegratioTestWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public HealthCheckTests(IntegratioTestWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task HealthCheck_ReturnOk()
        {
            var response = await _httpClient.GetAsync("/healthcheck");

            response.EnsureSuccessStatusCode();
        }
    }
}