using System;
using System.Linq;
using TennisBookings.Merchandise.Api.DomainModels;

namespace TennisBookings.Merchandise.Api.Data
{
    public class ProductValidator : IProductValidator
    {
        private readonly ICategoryProvider _categoryProvider;

        public ProductValidator(ICategoryProvider categoryProvider) => _categoryProvider = categoryProvider;

        public ValidationResult ValidateNewProduct(Product product)
        {
            _ = product ?? throw new ArgumentNullException(nameof(product), "A product is required");

            var result = new ValidationResult();

            if (product.Id == default)
                result.Errors.Add(nameof(product.Id), "A non-default ID is required.");

            if (string.IsNullOrEmpty(product.Name))
                result.Errors.Add(nameof(product.Name), "A name is required.");

            if (product.Name.Length > 256)
                result.Errors.Add(nameof(product.Name), "The name is too long.");

            if (string.IsNullOrEmpty(product.ShortDescription))
                result.Errors.Add(nameof(product.ShortDescription), "A description is required.");

            if (string.IsNullOrEmpty(product.Category))
                result.Errors.Add(nameof(product.Category), "A category is required.");

            if (!_categoryProvider.AllowedCategories().Contains(product.Category))
                result.Errors.Add(nameof(product.Category), "The category did not match any of the allowed categories.");

            if (product.Price is null)
                result.Errors.Add(nameof(product.Price), "A price is required.");

            if (product.Ratings.Any())
                result.Errors.Add(nameof(product.Ratings), "A new product cannot have ratings.");

            if (string.IsNullOrEmpty(product.InternalReference))
                result.Errors.Add(nameof(product.InternalReference), "An internal reference is required.");

            return result;
        }
    }
}
