using Boutique.Entities.Concrate;

namespace Boutique.Models
{
    public class AdminIndexViewModel
    {

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
