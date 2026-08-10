using Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Login(string email, string password, bool check)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, check, lockoutOnFailure: false);

            if (result.Succeeded)
            {

                var user = await _userManager.FindByEmailAsync(email);

                if (user.HotelId == null)
                {
                    return View();
                }

                var claim = new Claim("hotelId", user.HotelId.ToString());
                await _userManager.AddClaimAsync(user, claim);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View();
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