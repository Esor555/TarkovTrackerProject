using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TarkovTrackerBLL.Tasks;
using BaseObjects.DTO;
using Microsoft.AspNetCore.Authentication;
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

            var result = _taskLogin.Authenticate(UserDto);

            if (!result.Success)
            {
                ErrorMessage = string.Join("<br/>", result.Errors);
                return Page();
            }

            var user = result.User!;
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