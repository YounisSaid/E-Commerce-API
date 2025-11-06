using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Products;
using E_Commerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.DbInitializers
{
    public class DbInititializer(StoreDbContext context) : IDbInititializer
    {
        public async Task Inititialize()
        {
            await context.Database.MigrateAsync();

            if(!context.productBrands.Any())
            {
               var brandsData =await File.ReadAllTextAsync(@"..\E-Commerce.Persistence\Context\DataSeed\brands.json");

                var opt = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData,opt);

                if(brands.Any()&&brands is not null )
                {
                    context.AddRange(brands);
                }

                await context.SaveChangesAsync();             
            }
            if(!context.ProductTypes.Any())
            {
                var typesData = await File.ReadAllTextAsync(@"..\E-Commerce.Persistence\Context\DataSeed\types.json");

                var opt = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData, opt);

                if (types.Any() && types is not null)
                {
                    context.AddRange(types);
                }

                await context.SaveChangesAsync();
            }
            if (!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync(@"..\E-Commerce.Persistence\Context\DataSeed\products.json");

                var opt = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var products = JsonSerializer.Deserialize<List<Product>>(productsData, opt);

                if (products.Any() && products is not null)
                {
                    context.AddRange(products);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
