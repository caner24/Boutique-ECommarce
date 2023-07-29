using Boutique.Entities.Concrate;

namespace Boutique.Models
{
    public class ProductDetailViewModel
    {

        public int ProductId { get; set; }


        public string ProductName { get; set; }

        public string ProductDetailText { get; set; }

        public int Price { get; set; }

        public string Color { get; set; }
        public List<Photos> Photos { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
