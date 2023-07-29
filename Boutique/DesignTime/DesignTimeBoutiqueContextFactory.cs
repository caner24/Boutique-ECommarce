using Boutique.DataAccess.Concrate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Boutique.DesignTime
{
    public class DesignTimeBoutiqueContextFactory : IDesignTimeDbContextFactory<BoutiqueContext>
    {
        public BoutiqueContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

            var builder = new DbContextOptionsBuilder<BoutiqueContext>();
            var connectionstring = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionstring);
            return new BoutiqueContext(builder.Options);
        }
    }
}
