using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShopCore.Web.Api.Models.Products;
using MyShopCore.Web.Api.Services.Foundations.Products;

namespace MyShopCore.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = this.productService.RetrieveAllProducts().ToList();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetSingleProduct")]
        public async ValueTask<IActionResult> GetProductAsync(Guid id)
        {
            var product = 
                await this.productService.RetrieveProductByIdAsync(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostProduct([FromBody] Product product)
        {
            var newProduct =
                await this.productService.AddProductAsync(product);

            return Created("GetSingleProduct", newProduct);
        }
    }
}
