using Boutique.Entities.Concrate;
using System.ComponentModel.DataAnnotations;

namespace Boutique.Models
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Lütfen Fiyat bilginizi boş bırakmayın !.")]
        public int Price { get; set; }
        public enum IsStock
        {
            yes,
            no,
        }
        public List<Category> Categories { get; set; }
    }
}
