using Admin.Models;
using Domain.Models.Roles;
using Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UsersController(UserManager<User>userManager,RoleManager<Role> roleManager)
        {                   
            _roleManager = roleManager;
            _userManager = userManager;

        }

        public async Task<IActionResult>Index()
        {
            var users = _userManager.Users;
            return View(await users.ToListAsync());
        }

        public async Task<IActionResult> CreateClaims()
        {
            var user = await _userManager.FindByNameAsync("username");
            if (user != null) 
            {
                var claim = new Claim("Permission", "CanEdit");
                var result = await _userManager.AddClaimAsync(user, claim);

                if (result.Succeeded)
                {
                    return Ok("Claim added successfully.");
                }
            }

            return BadRequest("Error adding claim.");


        }




        public async Task<IActionResult> RemoveClaims()
        {
            var user = await _userManager.FindByNameAsync("username");
            if (user != null)
            {
                var claim = new Claim("Permission", "CanEdit");
                var result = await _userManager.RemoveClaimAsync(user, claim);

                if (result.Succeeded)
                {
                    return Ok("Claim removed successfully.");
                }
            }

            return BadRequest("Error adding claim.");


        }



        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ManageUserRolesViewModel
            {
                UserId = userId,
                AvailableRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync(),
                AssignedRoles = await _userManager.GetRolesAsync(user)
            };

            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> ManageRoles(ManageUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = model.SelectedRoles.Except(userRoles);
            var rolesToRemove = userRoles.Except(model.SelectedRoles);


            await _userManager.AddToRolesAsync(user, rolesToAdd);
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            return RedirectToAction("Index");
        }
    }
}
