using Boutique.DataAccess.Concrate.Configuration;
using Boutique.Entities.Concrate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.DataAccess.Concrate
{
    public class BoutiqueContext : IdentityDbContext<User>
    {
        public BoutiqueContext()
        {

        }
        public BoutiqueContext(DbContextOptions<BoutiqueContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Adress> Adress { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Product> Product { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductDetail> ProductDetail { get; set; }
        public DbSet<NewsLetter> NewsLetter { get; set; }
        public DbSet<NewsLetterUser> NewsLetterUser { get; set; }
        public DbSet<Visits> Visits { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(async message => Debug.WriteLine(message), LogLevel.Warning)
                                .EnableSensitiveDataLogging();

            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(@"");
                base.OnConfiguring(optionsBuilder);
            }
        }

    }
}
