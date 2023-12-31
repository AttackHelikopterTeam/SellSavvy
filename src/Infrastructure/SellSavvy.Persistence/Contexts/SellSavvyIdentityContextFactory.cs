using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SellSavvy.Persistence.Configurations;

namespace SellSavvy.Persistence.Contexts
{
   public class SellSavvyIdentityContextFactory : IDesignTimeDbContextFactory<SellSavvyIdentityContext>
        {
            public SellSavvyIdentityContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var optionsBuilder = new DbContextOptionsBuilder<SellSavvyIdentityContext>();

                var connectionString = ConfigurationsDb.GetString("ConnectionStrings:PostgreSQL");

                optionsBuilder.UseNpgsql(connectionString);

                return new SellSavvyIdentityContext(optionsBuilder.Options);
            }
        }

}
