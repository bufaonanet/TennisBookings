using System;
using System.Collections.Generic;
using System.Linq;
using TennisBookings.Merchandise.Api.DomainModels;

namespace TennisBookings.Merchandise.Api.Data.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string InternalReference { get; set; }
        public int StockCount { get; set; }
        public ICollection<int> Ratings { get; set; } = new List<int>();
        public bool IsEnabled { get; set; }
        public DateTime CreatedUtc { get; set; }

        public static ProductDto FromProduct(Product product) => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            ShortDescription = product.ShortDescription,
            Category = product.Category,
            Price = product.Price.Value,
            InternalReference = product.InternalReference,
            StockCount = product.StockCount,
            Ratings = product.Ratings.Select(r => r.Score).ToArray(),
            IsEnabled = product.IsEnabled,
            CreatedUtc = product.CreatedUtc,
        };

        public Product ToProduct()
        {
            var product = new Product(Id)
            {
                Name = Name,
                ShortDescription = ShortDescription,
                Category = Category,
                Price = new GbpPrice(Price),
                InternalReference = InternalReference,
                Ratings = Ratings.Select(r => new Rating(r)).ToArray(),
                IsEnabled = IsEnabled,
                CreatedUtc = CreatedUtc,
            };

            product.ReceiveStock(StockCount);

            return product;
        }
    }
}
