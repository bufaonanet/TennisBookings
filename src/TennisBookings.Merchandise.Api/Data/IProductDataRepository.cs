using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TennisBookings.Merchandise.Api.DomainModels;

namespace TennisBookings.Merchandise.Api.Data
{
    public interface IProductDataRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<IReadOnlyCollection<Product>> GetProductsAsync();
        Task<IReadOnlyCollection<Product>> GetProductsForCategoryAsync(string category);
        Task<AddProductResult> AddProductAsync(Product product);
    }
}
