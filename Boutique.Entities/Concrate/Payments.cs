using Boutique.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Entities.Concrate
{
    public class Payments : IEntity
    {
        public int Id { get; set; }
        public enum Types
        {
            Advance,
            Installment,
        }
        public DateTime Date_ { get; set; }
        public double LineTotal { get; set; }
    }
}
