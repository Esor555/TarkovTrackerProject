using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarkovTrackerDAL.test
{
    public class LoginSend
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public LoginSend(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

}
