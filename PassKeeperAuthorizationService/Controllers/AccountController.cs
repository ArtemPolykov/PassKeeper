using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PassKeeperAuthorizationService.ViewModels;
using PassKeePerLib.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using PassKeeperAuthorizationService.Configuration;

namespace PassKeeperAuthorizationService.Controllers
{
	[Controller]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly TokenParametres _tokenParametres;

        public AccountController(UserManager<Users> userManager, TokenParametres tokenParametres)
        {
            _userManager = userManager;
            _tokenParametres = tokenParametres;
        }

        [HttpPost]
        public async Task<IActionResult> CheckToken(string userName, string token)
        {
            // Попытка создать токен из строки
            JwtSecurityToken t = null;
            try
            {
                t = new JwtSecurityToken(token);
            }
            catch   // Если не удалось создать токен, вернуть клиенту ошибку
            {
                ModelState.AddModelError("Token", "Invalid TokenString");
                return BadRequest(ModelState);
            }

            // Найти юзера в базе
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                ModelState.AddModelError("Login", "Invalid UserName");
                return BadRequest(ModelState);
            }

            // Если токен просрочен, вернуть ошибку
            if(t.ValidTo < DateTime.UtcNow)
            {
                ModelState.AddModelError("Token", "Invalid LifeTime");
                return BadRequest(ModelState);
            }

            if(! await _userManager.VerifyTwoFactorTokenAsync(user, _tokenParametres.TokenProvider, token))
            {
                ModelState.AddModelError("Token", "Invalid TokenString");
                return BadRequest(ModelState);
            }


            // Сгенерировать новый токен
            token = await _userManager.GenerateTwoFactorTokenAsync(user, _tokenParametres.TokenProvider);
            return Json(new { Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
             if(!ModelState.IsValid)
                 return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user != null)
            {
                ModelState.AddModelError("UserName", "UserName is used");
                return BadRequest(ModelState);
            }

            user = await _userManager.FindByEmailAsync(model.Email);
            if(user != null)
            {
                ModelState.AddModelError("Email", "Email is used");
                return BadRequest(ModelState);
            }

            user = new Users()
            {
                Email = model.Email,
                UserName = model.UserName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                foreach(var e in result.Errors)
                    ModelState.AddModelError(e.Code, e.Description);

                return BadRequest(ModelState);
            }

            // Сгенерировать новый токен
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, _tokenParametres.TokenProvider);
            return Json(new { Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
             if(!ModelState.IsValid)
                 return BadRequest(ModelState);

            // Попытаться получить пользователя по UserName или Email
            var user = await _userManager.FindByNameAsync(model.Login);
            if(user == null)
                user = await _userManager.FindByEmailAsync(model.Login);

            // Если пользователь не найден
            if(user == null)
            {
                ModelState.AddModelError("Login", "Invalid login");
                return BadRequest(ModelState);
            }

            // Проверить пароль
            if(!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError("Login", "Invalid Password");
                return BadRequest(ModelState);
            }

            // Сгенерировать новый токен
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, _tokenParametres.TokenProvider);
            return Json(new { Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> SignOut(string token)
        {
            var t = new JwtSecurityToken(token);
            var userName = t?.Claims?.FirstOrDefault(c => c.Type == "UserName")?.Value;
            // Если не удалось получить токен или его структура неверна
            if(userName == null)
            {
                ModelState.AddModelError("Token", "Invalid token");
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(userName);
            // Если пользователь не найден
            if(user == null)
            {
                ModelState.AddModelError("Token", "Invalid login");
                return BadRequest(ModelState);
            }

            // Проверить что разлогиниться хочет юзер с актуальным токеном
            if(!await _userManager.VerifyTwoFactorTokenAsync(user, _tokenParametres.TokenProvider, token))
            {
                ModelState.AddModelError("Token", "Invalid token");
                return BadRequest(ModelState);
            }

            // Удалить токен из базы
            var result = await _userManager.RemoveAuthenticationTokenAsync(
                user, _tokenParametres.LoginProvider, _tokenParametres.TokenName);

            // Если не вышло удалить токен
            if(!result.Succeeded)
            {
                ModelState.AddModelError("SignOut", "Can not reset token");
                return BadRequest(ModelState);
            }

            return StatusCode(200);
        }
    }
}