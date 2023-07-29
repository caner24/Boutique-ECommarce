using Boutique.Entities.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.DataAccess.Concrate.Configuration
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.HasKey(x =>x.Id);
            builder.HasIndex(x => x.Fieche_No).IsUnique();
            builder.Property(x => x.Fieche_No).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
        }
    }
}
