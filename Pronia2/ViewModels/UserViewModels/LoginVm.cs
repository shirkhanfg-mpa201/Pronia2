using System.ComponentModel.DataAnnotations;

namespace Pronia2.ViewModels.UserViewModels
{
    public class LoginVm
    {
        [Required, EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [Required, MinLength(6), MaxLength(256), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;


        public bool RememberMe { get; set; }
        }
}
