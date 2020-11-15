namespace PassKeeperAuthorizationService.ViewModels
{
    // Модель передается сервису авторизации и аутентификации 
    // при попрытке зарегистрироваться.
    public class SignUpModel
    {
        public string UserName { get; set; }
        public string Email{ get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}