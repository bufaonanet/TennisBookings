using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TennisBookings.Merchandise.Api.Core;
using TennisBookings.Merchandise.Api.Data;
using TennisBookings.Merchandise.Api.Models.Input;
using TennisBookings.Merchandise.Api.Models.Output;

namespace TennisBookings.Merchandise.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductDataRepository _productDataRepository;

        public ProductsController(IProductDataRepository productDataRepository)
        {
            _productDataRepository = productDataRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductOutputModel[]>> GetAll()
        {
            var products = await _productDataRepository.GetProductsAsync();

            return products.Select(p => ProductOutputModel.FromProduct(p)).ToArray();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductOutputModel>> Get([Required]Guid id)
        {
            var product = await _productDataRepository.GetByIdAsync(id);

            if (product is null)
                return NotFound();

            return ProductOutputModel.FromProduct(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]        
        public async Task<IActionResult> Post(ProductInputModel model)
        {
            var product = model.ToProduct();

            var addResult = await _productDataRepository.AddProductAsync(product);

            if (addResult.IsSuccess)
            {
                return CreatedAtAction(nameof(Get), new { id = product.Id }, ProductOutputModel.FromProduct(product));
            }

            if (addResult.IsDuplicate)
            {
                var existingUrl = Url.Action(nameof(Get),
                    ControllerContext.ActionDescriptor.ControllerName,
                    new { id = product.Id },
                    Request.Scheme,
                    Request.Host.ToUriComponent());

                HttpContext.Response.Headers.Add("Location", existingUrl);
                return StatusCode(StatusCodes.Status409Conflict);
            }

            ModelState.AddValidationResultErrors(addResult.ValidationResult.Errors);

            return ValidationProblem();
        }
    }
}
