using E_commerce.Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Persistence.Context.Configurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(o => o.ShortName)
                   .HasColumnType("varchar")
                   .HasMaxLength(128);
            builder.Property(o => o.DeliveryTime)
                   .HasColumnType("varchar")
                   .HasMaxLength(128);
            
            builder.Property(o => o.Description)
                   .HasColumnType("varchar")
                   .HasMaxLength(256);

            builder.Property(o => o.Price)
                   .HasColumnType("decimal(18,2)")
                   .HasMaxLength(128);
            


        }
    }
}
