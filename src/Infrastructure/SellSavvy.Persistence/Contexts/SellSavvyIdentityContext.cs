using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SellSavvy.Domain.Common;
using SellSavvy.Domain.Entities;
using SellSavvy.Domain.Identity;
using SellSavvy.Persistence.Configurations.Entities;
using SellSavvy.Persistence.Configurations.Identity;
using System.Reflection;

namespace SellSavvy.Persistence.Contexts
{
    public class SellSavvyIdentityContext : IdentityDbContext<Person, Role, Guid>
    {
        public override DbSet<Person> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductPerson> ProductPersons { get; set; }






        public SellSavvyIdentityContext(DbContextOptions<SellSavvyIdentityContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    ((ICreatedByEntity)entry.Entity).CreatedOn = DateTime.UtcNow;
                }
                if (entry.State == EntityState.Modified)
                {
                    ((IModifiedByEntity)entry.Entity).LastModifiedOn = DateTime.UtcNow;
                }
            }


            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration()); 
            modelBuilder.ApplyConfiguration(new ProductPersonConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
        
            base.OnModelCreating(modelBuilder);

        }


    }
}
