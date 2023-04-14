using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TennisBookings.Merchandise.Api.Data;
using TennisBookings.Merchandise.Api.Models.Output;
using TennisBookings.Merchandise.Api.Stock;

namespace TennisBookings.Merchandise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IProductDataRepository _productDataRepository;
        private readonly IStockCalculator _stockCalculator;

        public StockController(
            IProductDataRepository productDataRepository,
            IStockCalculator stockCalculator)
        {
            _productDataRepository = productDataRepository;
            _stockCalculator = stockCalculator;
        }
        [HttpGet("total")]
        public async Task<ActionResult<StockOutputModel>> GetStockTotal()
        {
            var produtct = await _productDataRepository.GetProductsAsync();
            var totalStockCount = _stockCalculator.CalculateStockTotal(produtct);

            return Ok(new StockOutputModel { StockItemTotal = totalStockCount });
        }
    }
}
