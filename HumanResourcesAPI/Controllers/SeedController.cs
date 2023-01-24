using Microsoft.AspNetCore.Identity;
using System.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HumanResourcesAPI.Data;
using HumanResourcesAPI.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace HumanResourcesAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SeedController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;

    public SeedController(
        ApplicationDbContext context,
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IWebHostEnvironment env,
        IConfiguration configuration)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
        _env = env;
        _configuration = configuration;
    }

    

    [HttpGet]
    public async Task<ActionResult> CreateDefaultUsers()
    {
        string role_RegisteredUser = "RegisteredUser";
        string role_Administrator = "Administrator";

        if (await _roleManager.FindByNameAsync(role_RegisteredUser) == null)
        {
            await _roleManager.CreateAsync(new IdentityRole(role_RegisteredUser));
        }

        if (await _roleManager.FindByNameAsync(role_Administrator) == null)
        {
            await _roleManager.CreateAsync(new IdentityRole(role_Administrator));
        }

        var addedUserList = new List<ApplicationUser>();

        var email_Admin = "admin@email.com";

        if (await _userManager.FindByNameAsync(email_Admin) == null)
        {
            var user_Admin = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = email_Admin,
                Email = email_Admin
            };

            await _userManager.CreateAsync(user_Admin, _configuration["DefaultPasswords:Administrator"]);

            await _userManager.AddToRoleAsync(user_Admin, role_RegisteredUser);
            await _userManager.AddToRoleAsync(user_Admin, role_Administrator);

            user_Admin.EmailConfirmed = true;
            user_Admin.LockoutEnabled = false;

            addedUserList.Add(user_Admin);
        }

        if (addedUserList.Count > 0)
            await _context.SaveChangesAsync();

        return new JsonResult(new
        {
            Count = addedUserList.Count,
            Users = addedUserList
        });
    }
}
