using Boutique.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Entities.Concrate
{
    public class NewsLetter:IEntity
    {
        public int Id { get; set; }
        public string SubTitle { get; set; }
        public DateTime _NewsDate { get; set; }
    }
}
