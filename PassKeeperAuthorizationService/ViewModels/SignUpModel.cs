using System.ComponentModel.DataAnnotations;

namespace PassKeeperAuthorizationService.ViewModels
{
    // Модель передается сервису авторизации и аутентификации 
    // при попрытке зарегистрироваться.
    public class SignUpModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}