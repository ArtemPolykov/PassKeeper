namespace PassKeeperAuthorizationService.ViewModels
{
    // Модель передается при попытке войти в систему
    public class SignInModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}