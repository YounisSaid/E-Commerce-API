using AutoMapper;
using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Products;
using E_Commerce.Serviece.Abstraction;
using E_Commerce.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Servieces
{
    public class ProductServiece(IUnitOfWork unitOfWork,IMapper mapper) : IProductServiece
    {
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync(int id)
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return  mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            return mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(int id)
        {
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();
            return mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<TypeDto>> GetTypesAsync(int id)
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return mapper.Map<IEnumerable<TypeDto>>(types);
        }
    }
}
