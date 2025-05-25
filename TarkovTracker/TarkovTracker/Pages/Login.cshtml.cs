using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Security.Claims;
using TarkovTrackerBLL.DTO;
using TarkovTrackerBLL.Service;
using TarkovTrackerBLL.Tasks;

namespace TarkovTracker.Pages
{
        public class LoginModel : PageModel
        {
            private readonly TaskLogin _taskLogin;

            [BindProperty] public UserDTO UserDto { get; set; } = new();

            public string ErrorMessage { get; set; }

            public LoginModel(IConfiguration configuration)
            {
                string connectionString = configuration["ConnectionStrings:1"];
            _taskLogin = new TaskLogin(connectionString);
            }

            public async Task<IActionResult> OnPost()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var user = _taskLogin.AuthenticateUser(UserDto.Username, UserDto.password);

                if (user == null)
                {
                    ErrorMessage = "Invalid username or password.";
                    return Page();
                }

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToPage("Index");
            }
        }
    }