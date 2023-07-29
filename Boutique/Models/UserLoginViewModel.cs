using System.ComponentModel.DataAnnotations;

namespace Boutique.Models
{
	public class UserLoginViewModel
	{

        public bool RememberMe { get; set; }
        [EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Şifreniz boş geçilemez.")]
		public string Password { get; set; }
	}
}
