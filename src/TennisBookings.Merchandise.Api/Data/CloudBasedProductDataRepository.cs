using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using SimulatedCloudSdk.Queue;
using TennisBookings.Merchandise.Api.Data.Dto;
using TennisBookings.Merchandise.Api.DomainModels;
using TennisBookings.Merchandise.Api.External.Database;
using TennisBookings.Merchandise.Api.External.Queue;
using TennisBookings.Merchandise.Api.External.Queue.Messages;

namespace TennisBookings.Merchandise.Api.Data
{
    /// <summary>
    /// This simulates creating and retrieving data from a cloud services, such as AWS DynamoDb or Azure CosmosDb, through an abstraction. 
    /// </summary>
    /// <remarks>
    /// Product data will reset each time you run the application.
    /// </remarks>
    public class CloudBasedProductDataRepository : IProductDataRepository
    {
        private readonly IProductValidator _productValidator;
        private readonly ICloudDatabase _cloudDatabase;
        private readonly ICloudQueue _cloudQueue;

        public CloudBasedProductDataRepository(IProductValidator productValidator, ICloudDatabase cloudDatabase, ICloudQueue cloudQueue)
        {
            _productValidator = productValidator;
            _cloudDatabase = cloudDatabase;
            _cloudQueue = cloudQueue;            
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await _cloudDatabase.GetAsync(id.ToString());
            return product is object ? product.ToProduct() : null;
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsAsync()
        {
            var allProducts = await _cloudDatabase.ScanAsync();
            return allProducts.Select(p => p.ToProduct()).ToArray();
        }

        public async Task<IReadOnlyCollection<Product>> GetEnabledProductsAsync()
        {
            var allProducts = await _cloudDatabase.ScanAsync();
            return allProducts.Where(p => p.IsEnabled).Select(p => p.ToProduct()).ToArray();
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsForCategoryAsync(string category)
        {
            var allProducts = await _cloudDatabase.ScanAsync();
            return allProducts.Where(p => string.Equals(p.Category, category, StringComparison.CurrentCultureIgnoreCase)).Select(p => p.ToProduct()).ToArray();
        }

        public async Task<AddProductResult> AddProductAsync(Product product)
        {
            _ = product ?? throw new ArgumentNullException(nameof(product), "A product is required");

            var validationResult = _productValidator.ValidateNewProduct(product);

            var exsiting = await _cloudDatabase.GetAsync(product.Id.ToString());

            if (validationResult.IsValid && exsiting is null)
            {
                await _cloudDatabase.InsertAsync(product.Id.ToString(), ProductDto.FromProduct(product));
                await _cloudQueue.SendAsync(CreateSendRequest(product));

                return new AddProductResult(validationResult, false);
            }

            return new AddProductResult(validationResult, validationResult.IsValid); // if it's valid here, then it's a duplicate
        }

        private SendRequest CreateSendRequest(Product product)
        {
            var message = new ProductMessage { EventName = "Created", ProductId = product.Id, EventDate = product.CreatedUtc };

            var request = new SendRequest
            {
                Body = JsonSerializer.Serialize(message)
            };

            return request;
        }  
    }
}
