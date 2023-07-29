using Boutique.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Entities.Concrate
{
    public class Adress:IEntity
    {
        public Adress()
        {
            Cities = new HashSet<City>();
            District = new HashSet<Districts>();
            Towns = new HashSet<Town>();
        }
        public int Id { get; set; }

        public string AdressText { get; set; }
        public   User Users { get; set; }
        public  ICollection<City> Cities { get; set; }
        public  ICollection<Districts> District { get; set; }
        public  ICollection<Town> Towns { get; set; }
    }
}
