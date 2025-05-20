using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarkovTrackerBLL.Service;

namespace TarkovTrackerBLL.Tasks
{
	public class Login
	{
		private readonly UserService _userService;
		public class login()
		{

		}
		public string ErrorMessage = string.Empty;
		public bool LoginValidator(string userName)
		{
		
			var user = _userService.GetByName(userName);
			if (user == null || !BCrypt.Net.BCrypt.Verify(user.PasswordHash, user.PasswordHash))
			{
				ErrorMessage = "Invalid username or password.";
				return false;
			}
			if (string.IsNullOrEmpty(user.Name) || user.Id == 0)
			{
				ErrorMessage = "Invalid user data.";
				return false;
			}

			ErrorMessage = string.Empty;
			return true;
		}
	}
}
