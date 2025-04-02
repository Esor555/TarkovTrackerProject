using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTBusinesLogic.DAL
{
	public class LoginSend
	{
		public string? Username { get; set; }
		public string? Password { get; set; }
		public LoginSend(string username, string password)
		{
			this.Username = username;
			this.Password = password;
		}
	}
	
}
