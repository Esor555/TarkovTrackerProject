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
        private readonly UserService _userService;
        private readonly Login login;

        [BindProperty] public UserDTO UserDto { get; set; } = new();

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

			if(login.LoginValidator(UserDto.Username)){
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
}