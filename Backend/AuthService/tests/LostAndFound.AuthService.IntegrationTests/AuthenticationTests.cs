using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace LostAndFound.AuthService.IntegrationTests
{
    public class AuthenticationTests : IClassFixture<IntegratioTestWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthenticationTests(IntegratioTestWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }


        [Theory]
        [InlineData("account/logout")]
        public async Task SecuredDeleteEndpoint_WithoutAuthorizationHeader_ReturnsUnauthorizedStatusCode(
            string url)
        {
            var response = await _client.DeleteAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            response.Headers.Contains("WWW-Authenticate").Should().BeTrue();
            response.Headers.WwwAuthenticate.ToString().Should()
                .BeEquivalentTo("Bearer");
        }

        [Theory]
        [InlineData("account/logout")]
        public async Task SecuredDeleteEndpoint_WithInvalidAuthorizationHeader_ReturnsUnauthorizedStatusCode(
            string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-token");

            var response = await _client.SendAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            response.Headers.Contains("WWW-Authenticate").Should().BeTrue();
            response.Headers.WwwAuthenticate.ToString().Should()
                .BeEquivalentTo("Bearer error=\"invalid_token\"");
        }

        [Theory]
        [InlineData("account/password")]
        public async Task SecuredPutEndpoint_WithoutAuthorizationHeader_ReturnsUnauthorizedStatusCode(
            string url)
        {
            var response = await _client.PutAsync(url, null!);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            response.Headers.Contains("WWW-Authenticate").Should().BeTrue();
            response.Headers.WwwAuthenticate.ToString().Should()
                .BeEquivalentTo("Bearer");
        }

        [Theory]
        [InlineData("account/password")]
        public async Task SecuredPutEndpoint_WithInvalidAuthorizationHeader_ReturnsUnauthorizedStatusCode(
            string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-token");

            var response = await _client.SendAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            response.Headers.Contains("WWW-Authenticate").Should().BeTrue();
            response.Headers.WwwAuthenticate.ToString().Should()
                .BeEquivalentTo("Bearer error=\"invalid_token\"");
        }
    }
}
