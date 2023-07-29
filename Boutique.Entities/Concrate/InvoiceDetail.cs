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
    public class InvoiceDetail : IEntity
    {
        public InvoiceDetail()
        {
            Products = new HashSet<Product>();
        }
        [Key, ForeignKey(nameof(Invoice))]
        public int Id { get; set; }

        public double LineTotal { get; set; }

        public double UnitPrice { get; set; }

        public Guid Fieche_No { get; set; }
        public  Invoices Invoice { get; set; }

        public  ICollection<Product> Products { get; set; }
    }
}
