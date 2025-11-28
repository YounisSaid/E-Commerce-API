using AutoMapper;
using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Products;
using E_commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.ApplicationServiceRegistration;
using E_Commerce.Service.Specifications;
using E_Commerce.Shared;
using E_Commerce.Shared.Dtos.Products;

namespace E_Commerce.Service.Services
{
    public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync(int id)
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return  mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specfs = new ProductWithBrandTypeSpecification(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specfs);
            if(product == null) throw new ProductNotFoundException(id);
            return mapper.Map<ProductDto>(product);
        }

        public async Task<PaginatedResult<ProductDto>> GetProductsAsync(ProductQueryParameters parameters)
        {
            var specfs = new ProductWithBrandTypeSpecification(parameters);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specfs);
            var result = mapper.Map<IEnumerable<ProductDto>>(products);
            
            var specCount = new ProductCountSpecification(parameters);
            int count = await unitOfWork.GetRepository<Product, int>().CountAsync(specCount);
            return new PaginatedResult<ProductDto>( parameters.PageIndex, parameters.PageSize, count, result);
        }

        public async Task<IEnumerable<TypeDto>> GetTypesAsync(int id)
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return mapper.Map<IEnumerable<TypeDto>>(types);
        }
    }
}
