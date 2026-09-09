using System.Security.Claims;
using Admin.Models;
using Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AHotel.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "کاربر پیدا نشد.");
                    return View(model);
                }

               
                var existingClaim = await _userManager.GetClaimsAsync(user);

                if (!existingClaim.Any(x => x.Type == "hotelId"))
                {
                    var claim = new Claim("hotelId", user.HotelId.ToString());
                    await _userManager.AddClaimAsync(user, claim);
                }

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ModelState.AddModelError("", "ایمیل یا رمز عبور اشتباه است.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}