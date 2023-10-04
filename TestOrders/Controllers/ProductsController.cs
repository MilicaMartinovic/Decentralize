using Microsoft.AspNetCore.Mvc;
using TestOrders.Attributes;
using TestOrders.ObjectModel.DTOs;
using TestOrders.ObjectModel.IService;

namespace TestOrders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("top-products")]
        public ActionResult<IEnumerable<ProductDTO>> GetTopProducts([FromQuery] [ValidateSortBy] string sortBy = "volume", [FromQuery] int count = 10)
        {
            var products = _productService.GetTopProducts(sortBy, count);
            return Ok(products);
        }
    }

}