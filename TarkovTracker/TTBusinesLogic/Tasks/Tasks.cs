using BaseObjects.BaseObject;
using BaseObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarkovTrackerBLL.Service;
using TarkovTrackerBLL.Validators;

namespace TarkovTrackerBLL.Tasks
{
    public class TaskLogin
    {
        private readonly UserService _userService;

        public TaskLogin(string connectionString)
        {
            _userService = new UserService(connectionString);
        }

        public LoginResult Authenticate(UserDTO dto)
        {
            var validator = new LoginValidator();
            var validation = validator.Validate(dto);

            if (!validation.IsValid)
            {
                return new LoginResult
                {
                    Success = false,
                    Errors = validation.Errors
                };
            }

            var user = _userService.GetByName(dto.Username);
            if (user == null || !PasswordHasher.VerifyPassword(dto.passwordhash, user.PasswordHash))
            {
                return new LoginResult
                {
                    Success = false,
                    Errors = new List<string> { "Invalid username or password." }
                };
            }

            return new LoginResult
            {
                Success = true,
                User = user
            };
        }
    }

}

