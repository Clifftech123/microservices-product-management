using Microsoft.AspNetCore.Mvc;
using ProductService.API.Interfaces;

namespace ConsumerService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumerController : ControllerBase
    {
        private readonly IProductService _productService;

        public ConsumerController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> GetProduct()
        {
            var proudcts = await _productService.GetAllProductsAsync();
            return Ok(proudcts);
        }
    }
}
