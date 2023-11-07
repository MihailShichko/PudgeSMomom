using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PudgeSMomom.Data;
using PudgeSMomom.Models;
using PudgeSMomom.ViewModels;

namespace PudgeSMomom.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private ApplicationDbContext _dbContext;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

       /* [HttpGet]
        public IActionResult Login()
        {
            var responce = new LoginVM();
            return View(responce);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = 
        }
       */
    }
}
