using E_Commerce.Shared;
using E_Commerce.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Serviece.Abstraction
{
    public interface IProductServiece
    {
        Task <ProductDto> GetProductByIdAsync (int id);
        Task<PaginatedResult<ProductDto>> GetProductsAsync(ProductQueryParameters parameters);
        Task<IEnumerable<TypeDto>> GetTypesAsync(int id);
        Task<IEnumerable<BrandDto>> GetBrandsAsync(int id);
    }
}
