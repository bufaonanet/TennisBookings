using System.Collections.Generic;
using System.Linq;
using TennisBookings.Merchandise.Api.DomainModels;

namespace TennisBookings.Merchandise.Api.Stock
{
    public class StockCalculator : IStockCalculator
    {
        public int CalculateStockTotal(IEnumerable<Product> products) => products.Sum(p => p.StockCount);
    }
}
