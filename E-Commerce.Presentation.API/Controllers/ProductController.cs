using E_Commerce.Persistence.Attributes;
using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.Dtos.Products;
using E_Commerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controllers
{
    [Authorize]
    public class ProductController(IServiceManager manger) : APIBaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [Cache]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync(int id)
        {
            var brands = await manger.ProductService.GetBrandsAsync(id);
            return Ok(brands);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await manger.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync([FromQuery]ProductQueryParameters parameters)
        {
            var products = await manger.ProductService.GetProductsAsync(parameters);
            return Ok(products);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypesAsync(int id)
        {
            var types = await manger.ProductService.GetTypesAsync(id);
            return Ok(types);
        }
    }
}
