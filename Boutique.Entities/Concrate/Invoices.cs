using Boutique.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Entities.Concrate
{
    public class Invoices : IEntity
    {
        public int Id { get; set; }
        public int LineTotal { get; set; }

        public DateTime Date_ { get; set; }

        public Guid FiecheNo { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public  InvoiceDetail InvoiceDetail { get; set; }
    }
}
