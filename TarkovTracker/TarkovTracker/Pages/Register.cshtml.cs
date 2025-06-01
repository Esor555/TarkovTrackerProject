using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using BaseObjects.ennums;
using TarkovTrackerBLL.DTO;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.Services;
using TarkovTrackerDAL.test;

namespace TarkovTracker.Pages
{
    public class Register : PageModel
    {
        private readonly UserService _userService;

        [BindProperty] public UserDTO UserDto { get; set; }
        public List<string> Errors { get; set; } = new();

        public Register(IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionStrings:1"];
            _userService = new UserService(connectionString);
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            var newUser = new BaseObjects.BaseObject.User
            {
                Name = UserDto.Username,
                Level = UserDto.Level ?? 0,
                Faction = UserDto.Faction ?? Faction.USEC,
                PasswordHash = UserDto.password,
                Role = UserDto.role ?? "user"
            };

            var result = _userService.RegisterUser(newUser);

            if (!result.Success)
            {
                Errors = result.Errors;
                return Page();
            }

            var registeredUser = _userService.GetByName(newUser.Name);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, registeredUser.Id.ToString()),
            new Claim(ClaimTypes.Name, registeredUser.Name),
            new Claim(ClaimTypes.Role, registeredUser.Role ?? "User")
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            TempData["SuccessMessage"] = "Account created and logged in!";
            return RedirectToPage("Index");
        }
    }

}