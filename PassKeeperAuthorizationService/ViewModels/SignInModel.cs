using System.ComponentModel.DataAnnotations;

namespace PassKeeperAuthorizationService.ViewModels
{
    // Модель передается при попытке войти в систему
    public class SignInModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}