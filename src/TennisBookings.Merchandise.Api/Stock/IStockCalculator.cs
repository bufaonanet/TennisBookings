using System.Collections.Generic;
using TennisBookings.Merchandise.Api.DomainModels;

namespace TennisBookings.Merchandise.Api.Stock
{
    public interface IStockCalculator
    {
        int CalculateStockTotal(IEnumerable<Product> products);
    }
}
