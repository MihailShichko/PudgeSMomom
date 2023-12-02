using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PudgeSMomom.Data;
using PudgeSMomom.Models;
using PudgeSMomom.ViewModels;
using System.Security.Claims;
using PudgeSMomom.ViewModels.AccountVMs;
using System.Text;
using PudgeSMomom.Services.Steam;
using Microsoft.Extensions.Options;
using PudgeSMomom.Helpers;

namespace PudgeSMomom.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ISteamService _steamService;

        public AccountController(ApplicationDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager, ISteamService steamService)
        {
            _steamService = steamService;
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var AccountVM = await _steamService.GetPlayerData("eyJTdWJqZWN0IjoiY2UzZTA3ZDgtNGZiMy00ZTJiLWFmY2QtZWU4M2ZhZTUxYzIyIiwiU3RlYW1JZCI6IjMwMzg5Njc3MyIsIm5iZiI6MTcwMTI5MDMyMywiZXhwIjoxNzMyODI2MzIzLCJpYXQiOjE3MDEyOTAzMjMsImlzcyI6Imh0dHBzOi8vYXBpLnN0cmF0ei5jb20ifQ", Convert.ToInt64(user.steamId));
            return View(AccountVM);
        }

        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.Email);    
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                }

                TempData["Error"] = "Wrong password";   
                return View(loginVM);   
            }

            TempData["Error"] = "Wrong Login";
            return View(loginVM);

        }

        public IActionResult Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) { return View(registerVM); }

            var user = await _userManager.FindByEmailAsync(registerVM.Email);
            if(user != null)
            {
                TempData["Error"] = "This name is already taken =(";
                return View(registerVM);
            }

            var newUser = new User()
            {
                Email = registerVM.Email,
                UserName = registerVM.UserName,
                steamId = registerVM.SteamId
            };

            var newUserResponce = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (newUserResponce.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            else
            {
                var builder = new StringBuilder();
                newUserResponce.Errors.ToList().ForEach(error => { builder.AppendLine(error.Description); });
                TempData["Error"] = builder.ToString();
                return View(registerVM);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
    }
}
