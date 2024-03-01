using AspNetCoreHero.ToastNotification.Abstractions;
using BlogWebsite.Models;
using BlogWebsite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INotyfService _notifySerivce;
        public UserController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              INotyfService notyfService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notifySerivce = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View(new LoginVM());
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == vm.Username);
            if (existingUser == null)
            {
                _notifySerivce.Error("Username does not exist");
                return View(vm);
            }

            var verifyPassword = await _userManager.CheckPasswordAsync(existingUser, vm.Password);
            if (!verifyPassword)
            {
                _notifySerivce.Error("Password does not match");
                return View(vm);
            }

            await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, true);
            _notifySerivce.Success("Login successful");
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }

    }
}
