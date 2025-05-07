using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TTBusinesLogic.BusinesLogic;
using TTBusinesLogic.DTO;

namespace TarkovTracker.Pages
{
	public class LoginModel : PageModel
	{
		private readonly UserService _userService;

		[BindProperty]
		public UserDTO UserDto { get; set; } = new();

		public string ErrorMessage { get; set; }

		public LoginModel(IConfiguration configuration)
		{
			string connectionString = configuration["ConnectionStrings:1"];
			_userService = new UserService(connectionString);
		}

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userService.GetByName(UserDto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(UserDto.password, user.PasswordHash))
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

            // Make sure user.Name and user.Id are not null before adding to claims
            if (string.IsNullOrEmpty(user.Name) || user.Id == 0)
            {
                ErrorMessage = "Invalid user data.";
                return Page();
            }

            // Create claims based on user data
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("Role", user.Role ?? "User")  // Default to "User" if role is null
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Sign in the user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            // Redirect to the homepage (or any other page you want)
            return RedirectToPage("Index");
        }

    }
}
