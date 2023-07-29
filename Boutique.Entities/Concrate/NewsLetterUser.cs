using Boutique.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Entities.Concrate
{
    public class NewsLetterUser:IEntity
    {
        public int Id { get; set; }
        public DateTime _SubmitDate { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
    }
}
