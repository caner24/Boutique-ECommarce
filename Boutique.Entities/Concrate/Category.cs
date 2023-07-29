using Boutique.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Entities.Concrate
{
    public class Category : IEntity
    {
        public Category()
        {
            Products = new HashSet<ProductCategory>();  
        }
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public  ICollection<ProductCategory> Products { get; set; }
    }
}
