using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SellSavvy.Domain.Entities;

namespace SellSavvy.Persistence.Configurations.Entities
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Properties
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(p => p.Image)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(p => p.Description)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(p => p.Price)
                .IsRequired(false)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ProductState)
                .IsRequired();


            builder.Property(p => p.SellerId)
                .IsRequired(false)
                .HasMaxLength(255);

            //Entity Base
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.CreatedByUserId).IsRequired(false);
            builder.Property(p => p.CreatedOn).IsRequired(false);
            builder.Property(p => p.ModifiedByUserId).IsRequired(false);
            builder.Property(p => p.LastModifiedOn).IsRequired(false);
            builder.Property(p => p.IsDeleted).IsRequired(false);
            builder.Property(p => p.DeletedByUserId).IsRequired(false);
            builder.Property(p => p.DeletedOn).IsRequired(false);

            //Relationships
            builder.HasMany(p => p.ProductCategories)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ProductPersons)
                .WithOne(pp => pp.Product)
                .HasForeignKey(pp => pp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
