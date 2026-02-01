using E_commerce.Domain.Entites;
using E_commerce.Domain.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Identity.Context
{
    public class IdentityStoreDbContext(DbContextOptions<IdentityStoreDbContext> options): IdentityDbContext<AppUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(entity => entity.ToTable(name: "Users"));
            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "Users");

                entity.OwnsOne(u => u.Address);
                
            });
            builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Role"));
            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRole"));

            //Ignoring Unused Identity Tables
            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityRoleClaim<string>>();
            builder.Ignore<IdentityUserToken<string>>();



        }
    }
}
