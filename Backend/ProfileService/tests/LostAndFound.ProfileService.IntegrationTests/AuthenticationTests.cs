using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace LostAndFound.ProfileService.IntegrationTests
{
    public class AuthenticationTests : IClassFixture<IntegratioTestWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthenticationTests(IntegratioTestWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }


        [Theory]
        [InlineData("profile")]
        [InlineData("profile/2b1bafcd-b2fd-492b-b050-9b7027653716")]
        [InlineData("profile/2b1bafcd-b2fd-492b-b050-9b7027653716/comments")]
        public async Task SecuredGetEndpoint_WithoutAuthorizationHeader_ReturnsUnauthorizedStatusCode(
            string url)
        {
            var response = await _client.GetAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            response.Headers.Contains("WWW-Authenticate").Should().BeTrue();
            response.Headers.WwwAuthenticate.ToString().Should()
                .BeEquivalentTo("Bearer");
        }

        [Theory]
        [InlineData("profile")]
        [InlineData("profile/2b1bafcd-b2fd-492b-b050-9b7027653716")]
        [InlineData("profile/2b1bafcd-b2fd-492b-b050-9b7027653716/comments")]
        public async Task SecuredGetEndpoint_WithInvalidAuthorizationHeader_ReturnsUnauthorizedStatusCode(
            string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-token");

            var response = await _client.SendAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            response.Headers.Contains("WWW-Authenticate").Should().BeTrue();
            response.Headers.WwwAuthenticate.ToString().Should()
                .BeEquivalentTo("Bearer error=\"invalid_token\"");
        }

        [Theory]
        [InlineData("profile/picture")]
        public async Task SecuredPatchEndpoint_WithoutAuthorizationHeader_ReturnsUnauthorizedStatusCode(
            string url)
        {
            var response = await _client.PatchAsync(url, null!);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            response.Headers.Contains("WWW-Authenticate").Should().BeTrue();
            response.Headers.WwwAuthenticate.ToString().Should()
                .BeEquivalentTo("Bearer");
        }

        [Theory]
        [InlineData("profile/picture")]
        public async Task SecuredPatchEndpoint_WithInvalidAuthorizationHeader_ReturnsUnauthorizedStatusCode(
            string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Patch, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-token");

            var response = await _client.SendAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            response.Headers.Contains("WWW-Authenticate").Should().BeTrue();
            response.Headers.WwwAuthenticate.ToString().Should()
                .BeEquivalentTo("Bearer error=\"invalid_token\"");
        }

        [Theory]
        [InlineData("profile/2b1bafcd-b2fd-492b-b050-9b7027653716/comments")]
        public async Task SecuredPostEndpoint_WithoutAuthorizationHeader_ReturnsUnauthorizedStatusCode(
            string url)
        {
            var response = await _client.PostAsync(url, null!);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            response.Headers.Contains("WWW-Authenticate").Should().BeTrue();
            response.Headers.WwwAuthenticate.ToString().Should()
                .BeEquivalentTo("Bearer");
        }

        [Theory]
        [InlineData("profile/2b1bafcd-b2fd-492b-b050-9b7027653716/comments")]
        public async Task SecuredPostEndpoint_WithInvalidAuthorizationHeader_ReturnsUnauthorizedStatusCode(
            string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-token");

            var response = await _client.SendAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            response.Headers.Contains("WWW-Authenticate").Should().BeTrue();
            response.Headers.WwwAuthenticate.ToString().Should()
                .BeEquivalentTo("Bearer error=\"invalid_token\"");
        }

        [Theory]
        [InlineData("profile")]
        [InlineData("profile/2b1bafcd-b2fd-492b-b050-9b7027653716/comments")]
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
        [InlineData("profile")]
        [InlineData("profile/2b1bafcd-b2fd-492b-b050-9b7027653716/comments")]
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

        [Theory]
        [InlineData("profile/picture")]
        [InlineData("profile/2b1bafcd-b2fd-492b-b050-9b7027653716/comments")]
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
        [InlineData("profile/picture")]
        [InlineData("profile/2b1bafcd-b2fd-492b-b050-9b7027653716/comments")]
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
    }
}
