using E_Commerce.Shared;
using E_Commerce.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Abstraction
{
    public interface IProductService
    {
        Task <ProductDto> GetProductByIdAsync (int id);
        Task<PaginatedResult<ProductDto>> GetProductsAsync(ProductQueryParameters parameters);
        Task<IEnumerable<TypeDto>> GetTypesAsync();
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
    }
}
