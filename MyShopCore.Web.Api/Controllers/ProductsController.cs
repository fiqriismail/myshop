using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShopCore.Web.Api.Models.Products;
using MyShopCore.Web.Api.Models.Products.Exceptions;
using MyShopCore.Web.Api.Services.Foundations.Products;

namespace MyShopCore.Web.Api.Controllers
{
    [Authorize]
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
            try
            {
                var product =
                await this.productService.RetrieveProductByIdAsync(id);

                return Ok(product);
            } 
            catch (InvalidProductIdException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullProductException ex) 
            { 
                return NotFound(ex.Message);
            }


            

            
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostProduct([FromBody] Product product)
        {
            var newProduct =
                await this.productService.AddProductAsync(product);

            return Created("GetSingleProduct", newProduct);
        }

        [HttpPut]
        public async ValueTask<IActionResult> PutProduct([FromBody] Product product)
        {
            var currentProduct = 
                await this.productService.RetrieveProductByIdAsync(product.Id);

            if (currentProduct is null)
                return NotFound();

            var updatedProduct = 
                await this.productService.ModifyProductAsync(product);

            return Ok(updatedProduct);
        }

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteProduct(Guid id)
        {
            var currentProduct =
                await this.productService.RetrieveProductByIdAsync(id);

            if(currentProduct is null)
                return NotFound();

            var deletedProduct =
                await this.productService.RemoveProductAsync(currentProduct);

            return NoContent();
        }
    }
}
