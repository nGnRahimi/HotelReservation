using System.Security.Claims;
using Admin.Models;
using Domain.Models.Roles;
using Domain.Models.Users;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Admin.Controllers;

public class UsersController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ApplicationDbContext _Context;

    public UsersController(UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationDbContext context = null)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _Context = context;
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



    #region نمایش هتل ها برای تخصیص به کاربر
    public async Task<IActionResult> SetHotel(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        ViewBag.userId = userId;
        ViewBag.hotels = new SelectList(await _Context.Hotels.ToListAsync(), "Id", "Name", user.HotelId ?? 0);
        return PartialView();
    }


    #endregion

    #region تخصیص هتل به کاربر


    [HttpPost]
    public async Task<IActionResult> SetHotel(string userId, int hotelId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        user.HotelId = hotelId;
        await _userManager.UpdateAsync(user);

        return RedirectToAction("Index");
    }
    #endregion
}
