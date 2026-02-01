using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites.Identity;
using E_commerce.Domain.Entites.Orders;
using E_commerce.Domain.Entites.Products;
using E_Commerce.Persistence.Context;
using E_Commerce.Persistence.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace E_Commerce.Persistence.DbInitializers
{
    public class DbInitializer(
        StoreDbContext context,
        IdentityStoreDbContext identityContext,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager) : IDbInitializer
    {
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task InitializeAsync()
        {
            await ApplyPendingMigrationsAsync(context);

            await SeedDataAsync<DeliveryMethod>(context.DeliveryMethods, "delivery.json");
            await SeedDataAsync<ProductBrand>(context.productBrands, "brands.json");
            await SeedDataAsync<ProductType>(context.ProductTypes, "types.json");
            await SeedDataAsync<Product>(context.Products, "products.json");

            await context.SaveChangesAsync();
        }

        public async Task InitializeIdentityAsync()
        {
            await ApplyPendingMigrationsAsync(identityContext);
            await SeedRolesAsync();
            await SeedAdminUsersAsync();
        }

        private static async Task ApplyPendingMigrationsAsync(DbContext dbContext)
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await dbContext.Database.MigrateAsync();
            }
        }

        private async Task SeedDataAsync<T>(DbSet<T> dbSet, string fileName) where T : class
        {
            if (await dbSet.AnyAsync()) return;

            var path = Path.Combine("..", "E-Commerce.Persistence", "Context", "DataSeed", fileName);
            if (!File.Exists(path)) return;

            var jsonData = await File.ReadAllTextAsync(path);
            var entities = JsonSerializer.Deserialize<List<T>>(jsonData, _jsonOptions);

            if (entities?.Any() == true)
            {
                await dbSet.AddRangeAsync(entities);
            }
        }

        private async Task SeedRolesAsync()
        {
            if (await roleManager.Roles.AnyAsync()) return;

            await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        private async Task SeedAdminUsersAsync()
        {
            if (await userManager.Users.AnyAsync()) return;

            var superAdmin = CreateAppUser("SuperAdmin", "superadmin@gmail.com");
            var admin = CreateAppUser("Admin", "admin@gmail.com");

            await CreateUserWithRoleAsync(superAdmin, "P@ssw0rd", "SuperAdmin");
            await CreateUserWithRoleAsync(admin, "P@ssw0rd", "Admin");
        }

        private static AppUser CreateAppUser(string name, string email)
        {
            return new AppUser
            {
                UserName = name,
                Email = email,
                DisplayName = name,
                PhoneNumber = "01234567890"
            };
        }

        private async Task CreateUserWithRoleAsync(AppUser user, string password, string role)
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}