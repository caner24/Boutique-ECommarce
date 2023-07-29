using Boutique.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Entities.Concrate
{
    public class Districts : IEntity
    {
        public Districts()
        {
            Adress = new HashSet<Adress>();
        }

        public int DistrictsId { get; set; }

        public string DistrictName { get; set; }

        public  ICollection<Adress> Adress { get; set; }
    }
}
