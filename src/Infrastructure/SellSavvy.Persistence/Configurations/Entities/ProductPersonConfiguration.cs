using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SellSavvy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellSavvy.Persistence.Configurations.Entities
{
    public class ProductPersonConfiguration : IEntityTypeConfiguration<ProductPerson>
    {
        public void Configure(EntityTypeBuilder<ProductPerson> builder)
        {
            builder.HasKey(pp => new { pp.ProductId, pp.PersonId });

            builder.HasOne(pp => pp.Product)
                .WithMany(p => p.ProductPersons)
                .HasForeignKey(pp => pp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pp => pp.Person)
                .WithMany(person => person.ProductPersons)
                .HasForeignKey(pp => pp.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
