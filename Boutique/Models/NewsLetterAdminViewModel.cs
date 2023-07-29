using Boutique.Entities.Concrate;

namespace Boutique.Models
{
    public class NewsLetterAdminViewModel
    {

        public string SubTitle { get; set; }

        public IEnumerable<NewsLetterUser> NewsLetterUsers { get; set; }
    }
}
