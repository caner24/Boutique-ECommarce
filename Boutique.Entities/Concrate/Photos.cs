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
    public class Photos : IEntity
    {
        public int Id { get; set; }

        public string PhotoName { get; set; }

        public byte[] PhotoArr { get; set; }

        public int ProductDetailId { get; set; }
        public  ProductDetail ProductDetail { get; set; }
    }
}
