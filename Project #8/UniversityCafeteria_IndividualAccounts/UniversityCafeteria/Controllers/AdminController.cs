using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniversityCafeteria.Models;

namespace UniversityCafeteria.Controllers;

[Authorize(Roles = "SuperAdmin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _users;
    private readonly RoleManager<IdentityRole> _roles;
    public AdminController(UserManager<ApplicationUser> users, RoleManager<IdentityRole> roles)
    {
        _users = users;
        _roles = roles;
    }

    public async Task<IActionResult> Index()
    {
        var result = new List<UserRoleViewModel>();
        foreach (var user in _users.Users.ToList())
        {
            var roles = await _users.GetRolesAsync(user);
            result.Add(new UserRoleViewModel
            {
                UserId = user.Id,
                Email = user.Email ?? user.UserName ?? "",
                CurrentRole = roles.FirstOrDefault() ?? "User"
            });
        }
        return View(result);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeRole(UserRoleViewModel model)
    {
        var allowed = new[] { "User", "Admin", "SuperAdmin" };
        if (!allowed.Contains(model.NewRole)) return BadRequest();
        var user = await _users.FindByIdAsync(model.UserId);
        if (user == null) return NotFound();

        // Prevent an administrator from accidentally removing their own SuperAdmin access.
        if (user.Id == _users.GetUserId(User) && model.NewRole != "SuperAdmin")
        {
            TempData["Error"] = "You cannot remove your own SuperAdmin role.";
            return RedirectToAction(nameof(Index));
        }

        var currentRoles = await _users.GetRolesAsync(user);
        var remove = await _users.RemoveFromRolesAsync(user, currentRoles);
        if (!remove.Succeeded) return BadRequest(string.Join("; ", remove.Errors.Select(e => e.Description)));
        var add = await _users.AddToRoleAsync(user, model.NewRole);
        if (!add.Succeeded) return BadRequest(string.Join("; ", add.Errors.Select(e => e.Description)));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _users.FindByIdAsync(id);
        if (user == null) return NotFound();
        if (user.Id == _users.GetUserId(User)) return BadRequest("You cannot delete your own account.");
        var result = await _users.DeleteAsync(user);
        if (!result.Succeeded) return BadRequest(string.Join("; ", result.Errors.Select(e => e.Description)));
        return RedirectToAction(nameof(Index));
    }
}
