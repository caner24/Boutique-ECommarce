using System.ComponentModel.DataAnnotations;

namespace Boutique.Models
{
	public class RegisterViewModel
	{

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Şifreniz boş geçilemez.")]
        public string Password { get; set; }
        [Compare("Password")]
		public string RePassword { get; set; }
    }
}
