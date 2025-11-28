using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Identity;
using E_commerce.Domain.Entites.Products;
using E_Commerce.Persistence.Context;
using E_Commerce.Persistence.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.DbInitializers
{
    public class DbInitializer(StoreDbContext context,
                               IdentityStoreDbContext identityContext,
                               UserManager<AppUser> userManager,
                               RoleManager<IdentityRole> roleManager) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            if (context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
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

        public async Task InitializeIdentityAsync()
        {
            //Create the migrations if they are not applied
            if (identityContext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
               await identityContext.Database.MigrateAsync();
            }

            //Seed Default Roles and Admin User
            if(!identityContext.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole("SuperAdmin"),
                    new IdentityRole("Admin")
                };
              
                await roleManager.CreateAsync(roles[0]);
                await roleManager.CreateAsync(roles[1]);
            }

            if(!identityContext.Users.Any())
            {
                var superAdmin = new AppUser
                {
                    UserName = "SuperAdmin",
                    Email = "superadmin@gmail.com",
                    DisplayName = "SuperAdmin",
                    PhoneNumber = "01234567890",

                };
                var admin = new AppUser
                {
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                    DisplayName = "Admin",
                    PhoneNumber = "01234567890",

                };

                await userManager.CreateAsync(superAdmin, "P@ssw0rd");
                await userManager.CreateAsync(admin, "P@ssw0rd");

                await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
