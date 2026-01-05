using System.ComponentModel.DataAnnotations;

namespace Pronia2.ViewModels.UserViewModels
{
    public class RegisterVm
    {
        [Required, MaxLength(32), MinLength(3)]
        public string LastName { get; set; } = string.Empty;
        [Required, MaxLength(32), MinLength(3)]
        public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(256), MinLength(3)]
        public string UserName { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [Required, MinLength(6), MaxLength(256), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required, MinLength(6), MaxLength(256), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
