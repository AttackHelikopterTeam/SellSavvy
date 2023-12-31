using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SellSavvy.Domain.Entities;

namespace SellSavvy.Persistence.Configurations.Entities
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
       {
           //Properties
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.HasMany(c => c.ProductCategories)
                .WithOne(pc => pc.Category)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            
            //Entity Base
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.CreatedByUserId).IsRequired(false);
            builder.Property(c => c.CreatedOn).IsRequired(false);
            builder.Property(c => c.ModifiedByUserId).IsRequired();
            builder.Property(c => c.LastModifiedOn).IsRequired(false);
            builder.Property(c => c.IsDeleted).IsRequired(false);
            builder.Property(c => c.DeletedByUserId).IsRequired(false);
            builder.Property(c => c.DeletedOn);



            //Relationships
            builder.HasMany(c => c.ProductCategories)
                .WithOne(pc => pc.Category)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

