using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TarkovTracker.Pages
{
	public class LogoutModel : PageModel
	{
		public async Task<IActionResult> OnGet()
		{
			// Sign out and clear the authentication cookie
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			// Redirect to the login page after logout
			return RedirectToPage("Login");
		}
	}
}