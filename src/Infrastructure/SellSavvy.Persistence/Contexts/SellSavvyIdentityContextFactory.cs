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

            // var connectionString = ConfigurationsDb.GetString("ConnectionStrings:PostgreSQL");
            var macConnectionString = "Server=91.151.83.102;Port=5432;Database=Anil_Akpinar_Test1.3;User,Id=ahmetkokteam;Password=obXRMG*U6rJ4R0cbHszpgEuFd";

                optionsBuilder.UseNpgsql(macConnectionString);

                return new SellSavvyIdentityContext(optionsBuilder.Options);
            }
        }

}
