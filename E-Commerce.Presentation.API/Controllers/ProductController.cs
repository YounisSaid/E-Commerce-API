using E_commerce.Domain.Entites.Products;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Serviece.Abstraction;
using E_Commerce.Shared.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.API.Controllers
{
    public class ProductController(IProductServiece productServiece) : APIBaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync(int id)
        {
            var brands = await productServiece.GetBrandsAsync(id);
            return Ok(brands);
        }
        [HttpGet]

        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await productServiece.GetProductByIdAsync(id);
            return Ok(product);
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync(int id)
        {
            var products = await productServiece.GetProductsAsync(id);
            return Ok(products);
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypesAsync(int id)
        {
            var types = await productServiece.GetTypesAsync(id);
            return Ok(types);
        }
    }
}
