using Boutique.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Entities.Concrate
{
    public class ProductDetail : IEntity
    {
        public ProductDetail()
        {
            Photos = new List<Photos>();
        }
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string ProductDetailText { get; set; }

        public int Amount { get; set; }

        public string Color { get; set; }
        public  Product Product { get; set; }
        public  List<Photos> Photos { get; set; }
    }
}
