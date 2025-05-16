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

        public List<BaseObjects.BaseObject.User> Users { get; set; } = new();

        [BindProperty] public UserDTO UserDto { get; set; }


        public Register(IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionStrings:1"];
            var userRepository = new UserRepository(connectionString);
            _userService = new UserService(connectionString);
        }


        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newUser = new BaseObjects.BaseObject.User()
            {
                Name = UserDto.Username,
                Level = (int)UserDto.Level,
                Faction = (Faction)UserDto.Faction,
                PasswordHash = UserDto.password,
                Role = UserDto.role
            };

            if (_userService.AddUser(newUser))
            {
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

            TempData["ErrorMessage"] = "Failed to register user.";
            return Page();
        }
    }
}