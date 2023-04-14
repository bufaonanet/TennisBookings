using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TennisBookings.Merchandise.Api.Data;
using TennisBookings.Merchandise.Api.Data.Dto;
using TennisBookings.Merchandise.Api.External.Database;
using TennisBookings.Merchandise.Api.IntegrationTests.Fakes;
using TennisBookings.Merchandise.Api.IntegrationTests.Helpers.Serialization;
using TennisBookings.Merchandise.Api.IntegrationTests.Models;
using Xunit;

namespace TennisBookings.Merchandise.Api.IntegrationTests.Controllers
{
    public class ProductsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ProductsControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/products/");

            _client = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_ReturnsExpectedArrayOfProducts()
        {
            var products = await _client.GetFromJsonAsync<ExpectedProductModel[]>("");

            var expectedProducts = _factory.FakeCloudDatabase.Products.Count;

            Assert.NotNull(products);
            Assert.Equal(expectedProducts, products.Count());
        }

        [Fact]
        public async Task Get_ReturnsExpectedProduct()
        {
            var firstProdutct = _factory.FakeCloudDatabase.Products.First();

            var product = await _client.GetFromJsonAsync<ExpectedProductModel>($"{firstProdutct.Id}");

            Assert.NotNull(product);
            Assert.Equal(firstProdutct.Name, product.Name);
        }

        [Fact]
        public async Task Post_WithoutName_ReturnsBadRequest()
        {
            var productInputModel = GetValidProductInputModel().CloneWith(m => m.Name = null);

            var response = await _client.PostAsJsonAsync(
                 requestUri: "", productInputModel, JsonSerializerHelper.DefaultSerialisationOption);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_WithInvalidName_ReturnsExpectedProblemDetails()
        {
            var productInputModel = GetValidProductInputModel().CloneWith(m => m.Name = null);

            var response = await _client.PostAsJsonAsync(
                requestUri: "", productInputModel, JsonSerializerHelper.DefaultSerialisationOption);

            var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            Assert.Collection(problemDetails.Errors, kvp =>
            {
                Assert.Equal("Name", kvp.Key);
                var error = Assert.Single(kvp.Value);
                Assert.Equal("The Name field is required.", error);
            });
        }

        private static TestProductInputModel GetValidProductInputModel(Guid? id = null)
        {
            return new TestProductInputModel
            {
                Id = id is object ? id.Value.ToString() : Guid.NewGuid().ToString(),
                Name = "Some Product",
                Description = "This is a description",
                Category = new CategoryProvider().AllowedCategories().First(),
                InternalReference = "ABC123",
                Price = 4.00m
            };
        }
    }
}
