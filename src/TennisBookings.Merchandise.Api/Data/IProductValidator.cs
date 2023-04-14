using TennisBookings.Merchandise.Api.DomainModels;

namespace TennisBookings.Merchandise.Api.Data
{
    public interface IProductValidator
    {
        ValidationResult ValidateNewProduct(Product product);
    }
}