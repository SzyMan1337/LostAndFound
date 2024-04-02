using FluentAssertions;
using LostAndFound.PublicationService.CoreLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace LostAndFound.PublicationService.IntegrationTests.Controllers
{
    public class CategoriesControllerTests : IClassFixture<IntegratioTestWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CategoriesControllerTests(IntegratioTestWebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/categories/");
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllCategoriesEndpoint_Should_ReturnExpectedResponseDto()
        {
            var categories = await _client.GetFromJsonAsync<IEnumerable<CategoryResponseDto>>("");

            categories.Should().NotBeNullOrEmpty();
        }
    }
}
