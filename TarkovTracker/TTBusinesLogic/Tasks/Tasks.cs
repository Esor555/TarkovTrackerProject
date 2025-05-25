using BaseObjects.BaseObject;
using BaseObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarkovTrackerBLL.Service;

namespace TarkovTrackerBLL.Tasks
{
    public class TaskLogin
    {
        private readonly UserService _userService;

        public TaskLogin(string connectionString)
        {
            _userService = new UserService(connectionString);
        }

        public User? AuthenticateUser(string username, string password)
        {
            var user = _userService.GetByName(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            if (string.IsNullOrEmpty(user.Name) || user.Id == 0)
                return null;

            return user;
        }
        
    }
    public class UserRole
    {
        
    }
}
