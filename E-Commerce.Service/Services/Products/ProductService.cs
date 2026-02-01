using AutoMapper;
using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Products;
using E_commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Specifications.Products;
using E_Commerce.Shared;
using E_Commerce.Shared.Dtos.Products;

namespace E_Commerce.Service.Services.Products
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<IEnumerable<TypeDto>> GetTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return mapper.Map<IEnumerable<TypeDto>>(types);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithBrandTypeSpecification(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specification);

            if (product is null)
                throw new ProductNotFoundException(id);

            return mapper.Map<ProductDto>(product);
        }

        public async Task<PaginatedResult<ProductDto>> GetProductsAsync(ProductQueryParameters parameters)
        {
            var products = await FetchPaginatedProductsAsync(parameters);
            var totalCount = await FetchProductCountAsync(parameters);

            var mappedProducts = mapper.Map<IEnumerable<ProductDto>>(products);

            return new PaginatedResult<ProductDto>(
                parameters.PageIndex,
                parameters.PageSize,
                totalCount,
                mappedProducts);
        }

        private async Task<IEnumerable<Product>> FetchPaginatedProductsAsync(ProductQueryParameters parameters)
        {
            var specification = new ProductWithBrandTypeSpecification(parameters);
            return await unitOfWork.GetRepository<Product, int>().GetAllAsync(specification);
        }

        private async Task<int> FetchProductCountAsync(ProductQueryParameters parameters)
        {
            var countSpecification = new ProductCountSpecification(parameters);
            return await unitOfWork.GetRepository<Product, int>().CountAsync(countSpecification);
        }
    }
}