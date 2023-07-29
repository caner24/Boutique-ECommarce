using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.DataAccess.Concrate.Configuration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
       new IdentityRole
       {
           Name = "Visitor",
           NormalizedName = "VISITOR"
       },
       new IdentityRole
       {
           Name = "Administrator",
           NormalizedName = "ADMINISTRATOR"
       });
        }
    }
}
