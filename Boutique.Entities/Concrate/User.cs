using Boutique.Core.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Entities.Concrate
{
    public class User : IdentityUser, IEntity
    {
        public List<Invoices> Invoices { get; set; }
        public  List<Adress> Adress { get; set; }
        public  List<Visits> Visits { get; set; }


    }
}
