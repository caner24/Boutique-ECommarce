using Boutique.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Boutique.Entities.Concrate
{
    public class Product : IEntity
    {
        public Product()
        {
            Categories = new HashSet<ProductCategory>();
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }
        public int Id { get; set; }

        public int Price { get; set; }
        public enum IsStock
        {
            yes,
            no,
        }
        public  ProductDetail ProductDetails { get; set; }
        public  ICollection<ProductCategory> Categories { get; set; }
        public  ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
