using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PassKeeperAuthorizationService.ViewModels;
using PassKeePerLib.Models;

namespace PassKeeperAuthorizationService.Controllers
{
	[Controller]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;

        public AccountController(UserManager<Users> userManager)
        {
            _userManager = userManager;
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

            var user = await _userManager.FindByNameAsync(model.Login);

            if(user == null)
                user = await _userManager.FindByEmailAsync(model.Login);

            if(user == null)
            {
                ModelState.AddModelError("Login", "Invalid login");
                return BadRequest(ModelState);
            }

            if(!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError("Login", "Invalid Password");
                return BadRequest(ModelState);
            }

            return Json(new { Token = "Token" });
        }

        [HttpPost]
        public async Task SignOut()
        {
            throw new NotImplementedException();
        }
    }
}