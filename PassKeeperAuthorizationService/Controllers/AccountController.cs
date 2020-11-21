using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PassKeeperAuthorizationService.ViewModels;
using PassKeePerLib.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Linq;

namespace PassKeeperAuthorizationService.Controllers
{
	[Controller]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly UserManager<Users> _userManager;

        private async Task<JwtSecurityToken> GetToken(Users user)
        {
            

            var token = new JwtSecurityToken(
                issuer: "PassKeperAccount",
                audience: "PassKeeperClient"
            );

            return token;
            // var tokenClaim = (await _userManager.GetClaimsAsync(user)).Where(c => c.Type == "JwtToken");
            // if(tokenClaim != )
            // throw new NotImplementedException();
        }

        public AccountController(UserManager<Users> userManager)
        {
            _userManager = userManager;
            _tokenHandler = new JwtSecurityTokenHandler();
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

            return Json(new { Token = "Token" });
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

            // Получить токен для пользователя
            var token = await GetToken(user);

            return Json(new { Token = _tokenHandler.WriteToken(token) });
        }

        [HttpPost]
        public async Task SignOut()
        {
            throw new NotImplementedException();
        }
    }
}