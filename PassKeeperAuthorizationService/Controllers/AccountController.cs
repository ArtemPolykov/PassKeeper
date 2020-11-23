using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PassKeeperAuthorizationService.ViewModels;
using PassKeePerLib.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using PassKeeperAuthorizationService.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Collections.Generic;

namespace PassKeeperAuthorizationService.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly UserManager<Users> _userManager;
        private readonly TokenParametres _tokenParametres;

        private JwtSecurityToken CreateNewToken(Users user)
        {
            var now = DateTime.UtcNow;
            var exp = now.AddMinutes(_tokenParametres.LifeTimeMinutes);
            var signingCredentials = new SigningCredentials(_tokenParametres.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("UserName", user.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: _tokenParametres.Issuser,
                audience: _tokenParametres.Audience,
                claims: claims,
                notBefore: now,
                expires: exp,
                signingCredentials: signingCredentials
            );

            return token;
        }

        private async Task<JwtSecurityToken> GetTokenWithUpdate(Users user)
        {
            var token = CreateNewToken(user);
            var claims = (await _userManager.GetClaimsAsync(user)).Where(c => c.Type == "Token");

            await _userManager.RemoveClaimsAsync(user, claims);
            await _userManager.AddClaimAsync(user, new Claim("Token", _tokenHandler.WriteToken(token)));

            return token;
        }

        public AccountController(UserManager<Users> userManager, TokenParametres tokenParametres)
        {
            _userManager = userManager;
            _tokenParametres = tokenParametres;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        [HttpPost]
        public async Task<IActionResult> CheckToken(string token)
        {
            JwtSecurityToken t = null;

            try
            {
                t = new JwtSecurityToken(token);
            }
            catch
            {
                ModelState.AddModelError("Token", "Invalid TokenString");
                return BadRequest(ModelState);
            }
            
            var userName = t?.Claims?.FirstOrDefault(c => c.Type == "UserName")?.Value;
            if(userName == null)
            {
                ModelState.AddModelError("Token", "Invalid TokenString");
                return BadRequest(ModelState);
            }

            if(t.ValidTo < DateTime.UtcNow)
            {
                ModelState.AddModelError("Token", "Invalid LifeTime");
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                ModelState.AddModelError("Token", "Invalid UserName");
                return BadRequest(ModelState);
            }

            var claim = (await _userManager.GetClaimsAsync(user)).FirstOrDefault(c => c.Type == "Token");
            if(claim == null)
            {
                ModelState.AddModelError("Token", "Invalid UserName");
                return BadRequest(ModelState);
            }

            if(token != claim.Value)
            {
                ModelState.AddModelError("Token", "Invalid Token");
                return BadRequest(ModelState);
            }

            return Json(new { Token = _tokenHandler.WriteToken(await GetTokenWithUpdate(user)) });
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

            // Получить токен для пользователя
            return Json(new { Token = _tokenHandler.WriteToken(await GetTokenWithUpdate(user)) });
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
            return Json(new { Token = _tokenHandler.WriteToken(await GetTokenWithUpdate(user)) });
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

            var claims = (await _userManager.GetClaimsAsync(user)).Where(c => c.Type == "Token");
            var result = await _userManager.RemoveClaimsAsync(user, claims);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("Operation", "OperationError");
                return BadRequest(ModelState);
            }

            return StatusCode(200);
        }
    }
}