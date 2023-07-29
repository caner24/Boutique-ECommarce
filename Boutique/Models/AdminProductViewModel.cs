using Boutique.Entities.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace Boutique.Models
{
    public class AdminProductViewModel 
    {
        public IEnumerable<Category> Categories { get; set; }

        public Product Product { get; set; }
    }
}
