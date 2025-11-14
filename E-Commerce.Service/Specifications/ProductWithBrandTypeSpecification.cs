using E_commerce.Domain.Entites.Products;
using E_Commerce.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Specifications
{
    public class ProductWithBrandTypeSpecification : BaseSpecificiation<Product>
    {
        public ProductWithBrandTypeSpecification(ProductQueryParameters parameters) : base(CreateCriteria(parameters))
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);

        }
        public ProductWithBrandTypeSpecification(int id) : base(x=>x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);

        }

        private static Expression<Func<Product,bool>> CreateCriteria(ProductQueryParameters parameters)
        {
            return x => (!parameters.BrandId.HasValue || x.BrandId == parameters.BrandId.Value)
                     && (!parameters.TypeId.HasValue || x.TypeId == parameters.TypeId.Value);
                       
        }
    }
}
