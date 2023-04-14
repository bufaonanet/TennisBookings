using System;
using System.Collections.Generic;
using System.Linq;
using TennisBookings.Merchandise.Api.DomainModels;

namespace TennisBookings.Merchandise.Api.Models.Output
{
    public class ProductOutputModel
    {
        private ProductOutputModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.ShortDescription;
            Category = product.Category;
            Price = product.Price.Value;
            Remaining = product.StockCount;
            Ratings = product.Ratings.Select(r => r.Score).ToArray();
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Category { get; }
        public decimal Price { get; }
        public int Remaining { get; set; }
        public IReadOnlyCollection<int> Ratings { get; } = Array.Empty<int>();

        public static ProductOutputModel FromProduct(Product product)
        {
            _ = product ?? throw new ArgumentNullException(nameof(product));

            return new ProductOutputModel(product);
        }
    }
}
