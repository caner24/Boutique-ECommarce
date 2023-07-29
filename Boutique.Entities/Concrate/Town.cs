using Boutique.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Entities.Concrate
{
    public class Town : IEntity
    {
        public Town()
        {
            Adress=new HashSet<Adress>();
        }
        public int Id { get; set; }

        public string TownName { get; set; }
        public  ICollection<Adress>Adress { get; set; }
    }
}
