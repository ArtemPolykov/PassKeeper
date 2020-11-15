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
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            return Json(_userManager);
        }

        [HttpPost]
        public async Task SignOut()
        {
            throw new NotImplementedException();
        }
    }
}