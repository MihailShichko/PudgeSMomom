using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PudgeSMomom.Data;
using PudgeSMomom.Models;
using PudgeSMomom.ViewModels;
using System.Security.Claims;
using PudgeSMomom.ViewModels.AccountVMs;

namespace PudgeSMomom.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(ApplicationDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.Email);    
            if(user == null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Advert");
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

            var newUserResponce = await _userManager.CreateAsync(newUser, registerVM.Password);//problemo
            if (newUserResponce.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            else
            {
                TempData["Error"] = "problem with creating user";
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
