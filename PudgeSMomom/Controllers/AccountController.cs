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
using Microsoft.AspNet.SignalR;

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

        public async Task<IActionResult> Profile(string id)
        {
            //var user = await _userManager.GetUserAsync(User)
            var user = await _userManager.FindByIdAsync(id);
            var profileData = await _steamService.GetPlayerData("9CF743B10F3FE10AC9D37133571407DA", Convert.ToInt64(user.steamId));
            var profile = new UserAccount
            {
                ProfileData = profileData,
                User = user
            };
            return View(profile);
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
                        return RedirectToAction("Profile", "Account", new {id = user.Id});
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

            return RedirectToAction("Profile", "Account", new { id = newUser.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
            var currentUser = await _userManager.GetUserAsync(User);     
            return View(_userManager.Users.Where(user => user.Id != currentUser.Id));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Can not delete user";
            }

            return RedirectToAction("Users");
        }
    }
}
