using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TennisBookings.Merchandise.Api.IntegrationTests.Helpers.Serialization;
using TennisBookings.Merchandise.Api.IntegrationTests.Models;
using Xunit;

namespace TennisBookings.Merchandise.Api.IntegrationTests.Controllers
{
    public class CategoriesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public CategoriesControllerTests(WebApplicationFactory<Startup> factory)
        {
            //_httpClient = factory.CreateDefaultClient(new Uri("http://localhost/api/categories"));
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/categories");
            _httpClient = factory.CreateClient();

        }

        //[Fact]
        //public async Task GetAll_ReturnsSuccessStatusCode()
        //{
        //    var response = await _httpClient.GetAsync("");

        //    response.EnsureSuccessStatusCode();
        //}

        //[Fact]
        //public async Task GetAll_ReturnsEexpectedMediaType()
        //{
        //    var response = await _httpClient.GetAsync("");

        //    Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        //}

        //[Fact]
        //public async Task GetAll_ReturnsContent()
        //{
        //    var response = await _httpClient.GetAsync("");

        //    Assert.NotNull(response.Content);
        //    Assert.True(response.Content.Headers.ContentLength > 0);
        //}

        //[Fact]
        //public async Task GetAll_ReturnsExpectedJson()
        //{
        //    var responseStram = await _httpClient.GetStreamAsync("");

        //    var model = await JsonSerializer.DeserializeAsync<ExpectedCategoriesModel>(responseStram,
        //        JsonSerializerHelper.DefaultDeserialisationOption);

        //    var expectedCategories = new List<string>  {
        //        "Bags",
        //        "Accessories",
        //        "Clothing",
        //        "Balls",
        //        "Rackets"
        //    };

        //    Assert.NotNull(model?.AllowedCategories);
        //    Assert.Equal(expectedCategories.OrderBy(c => c), model.AllowedCategories.OrderBy(c => c));
        //}

        [Fact]
        public async Task GetAll_ReturnsResponse()
        {
            var expectedCategories = new List<string>  {
                "Bags",
                "Accessories",
                "Clothing",
                "Balls",
                "Rackets"
            };

            var model = await _httpClient.GetFromJsonAsync<ExpectedCategoriesModel>("");

            Assert.NotNull(model?.AllowedCategories);
            Assert.Equal(expectedCategories.OrderBy(c => c), model.AllowedCategories.OrderBy(c => c));
        }

        [Fact]
        public async Task GetAll_SetsExpectedCacheControlHeader()
        {
            var response = await _httpClient.GetAsync("");

            var header = response.Headers.CacheControl;

            Assert.True(header.MaxAge.HasValue);
            Assert.Equal(TimeSpan.FromMinutes(5), header.MaxAge);
            Assert.True(header.Public);
        }
    }
}