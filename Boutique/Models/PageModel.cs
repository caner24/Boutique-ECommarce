using Boutique.Entities.Concrate;

namespace Boutique.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }

    }

    public class ProductListModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string GroupBy { get; set; }

        public string Categories { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
