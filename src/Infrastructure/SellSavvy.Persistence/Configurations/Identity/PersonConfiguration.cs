using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SellSavvy.Domain.Identity;

namespace SellSavvy.Persistence.Configurations.Identity
{
    public class PersonConfiguration:IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {

            // Primary key
            builder.HasKey(p => p.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(p => p.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(p => p.NormalizedEmail).HasName("EmailIndex");
                             
           // Maps to the AspNetUpers table
                
                            
           // A concurrency pokenpfor use with the optimistic concurrency checking
            builder.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
                             
           // Limit the sizepof cplumns to use efficient database types
            builder.Property(p => p.UserName).HasMaxLength(256);
            builder.Property(p => p.NormalizedUserName).HasMaxLength(256);
            builder.Property(p => p.Email).HasMaxLength(256);
            builder.Property(p => p.NormalizedEmail).HasMaxLength(256);
            builder.Property(p => p.PasswordHash).HasMaxLength(256).IsRequired(false);
            builder.Property(p => p.SecurityStamp).HasMaxLength(256).IsRequired(false);
            builder.Property(p => p.PhoneNumber).HasMaxLength(256).IsRequired(false);
            builder.Property(p => p.Gender).IsRequired();
            builder.Property(p => p.FullName).HasMaxLength(256).IsRequired(false);
            // The relationships between User and other entity types

            builder.HasMany(p => p.ProductPersons)
                .WithOne(pp => pp.Person)
                .HasForeignKey(pp => pp.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
