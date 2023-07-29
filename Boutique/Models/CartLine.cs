using Boutique.Entities.Concrate;

namespace Boutique.Models
{
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Products { get; set; }
        public int Quantity { get; set; }
    }
}
