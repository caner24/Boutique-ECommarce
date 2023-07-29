using System.ComponentModel.DataAnnotations;

namespace Boutique.Models
{
    public class ForgotPasswordViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
