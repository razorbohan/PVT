using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using L6_P2_4_TagHelper.ViewModel;
using L6_P2_4_TagHelper.Models;
using Microsoft.AspNetCore.Identity;

namespace L6_P2_4_TagHelper.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<User> UserManager { get; private set; }
        public SignInManager<User> SignInManager { get; private set; }
        public AccountController(UserManager<User> manager, SignInManager<User> signInManager)
        {
            UserManager = manager;
            SignInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            string returnUrl = model.ReturnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                await SignInManager.SignOutAsync();

                //var user = await UserManager.FindByEmailAsync(model.Email);
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    return Redirect(returnUrl);
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = model.Email, UserName = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}