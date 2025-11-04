using E_commerce.Domain.Entites.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Context.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("VarChar")
                .HasMaxLength(256);

            builder.Property(p => p.Description)
                .HasColumnType("VarChar")
                .HasMaxLength(1024);

            builder.Property(p => p.PicutreURL)
                .HasColumnType("VarChar")
                .HasMaxLength(256);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(10,2)");

            builder.HasOne(p => p.ProductBrand)
                .WithMany()
                .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.TypeId);
        }
    }
}
