using System.Collections.Generic;
using System;

namespace TennisBookings.Merchandise.Api.IntegrationTests.Models
{
    public class ExpectedProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Remaining { get; set; }
        public IReadOnlyCollection<int> Ratings { get; set; }
    }
}
