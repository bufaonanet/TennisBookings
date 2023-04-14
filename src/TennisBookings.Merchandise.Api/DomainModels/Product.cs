using System;
using System.Collections.Generic;

namespace TennisBookings.Merchandise.Api.DomainModels
{
    public class Product
    {
        public Product(Guid id)
        {
            Id = id;
            CreatedUtc = DateTime.UtcNow; // use to demo IDateTime
        }

        public Guid Id { get; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Category { get; set; }
        public GbpPrice Price { get; set; }
        public string InternalReference { get; set; }
        public int StockCount { get; private set; }
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public bool IsEnabled { get; set; }
        public DateTime CreatedUtc { get; set; }

        public void ReceiveStock(int quantity)
        {
            if (quantity > 0)
            {
                StockCount += quantity;
            }
        }

        public bool SellItem()
        {
            if (StockCount <= 0)
            {
                return false;
            }

            StockCount--;
            return true;
        }
    }
}
