using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseObjects.BaseObject;
using BaseObjects.DTO;
using TarkovTrackerBLL.DTO;
using TarkovTrackerBLL.Validators;
using TarkovTrackerDAL.Services;
using TarkovTrackerDAL.test;
using UserDTO = BaseObjects.DTO.UserDTO;


namespace TarkovTrackerBLL.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(string connectionString)
        {
            _userRepository = new UserRepository(connectionString);
        }

        public User GetByName(string username)
        {
            if (username == null)
                throw new ArgumentException("Invalid user name");

            try
            {
                return _userRepository.GetByName(username);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving user with ID {username}", ex);
            }
        }
        public List<User> GetAllUsers()
        {
            try
            {
                return _userRepository.GetAll();
            }
            catch (Exception ex)
            {
                // Optionally log
                throw new ApplicationException("Error retrieving users", ex);
            }
        }

        public User GetUserById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid user ID");

            try
            {
                return _userRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving user with ID {id}", ex);
            }
        }

        public bool AddUser(User user)
        {

			UserDTO userDTO = new UserDTO(user.Id, user.Name, user.Level, user.Faction, PasswordHasher.HashPassword(user.PasswordHash),user.Role);
			
            if (string.IsNullOrWhiteSpace(user.Name))
                throw new ArgumentException("Username is required");

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new ArgumentException("Password is required");

            try
            {
                return _userRepository.Add(userDTO);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error adding user", ex);
            }
        }

        public bool DeleteUser(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid user ID");

            try
            {
                return _userRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error deleting user with ID {id}", ex);
            }
        }

        public bool UpdateUser(User user)
        {
	        UserDTO userDTO = new UserDTO(user.Id, user.Name, user.Level, user.Faction, PasswordHasher.HashPassword(user.PasswordHash), user.Role);
			if (user.Id <= 0)
                throw new ArgumentException("Invalid user ID");

            try
            {
                return _userRepository.Update(userDTO);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating user", ex);
            }
        }
        public RegisterUserDTO RegisterUser(User user)
        {
            var validator = new UserValidator();
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                return new RegisterUserDTO
                {
                    Success = false,
                    Errors = validationResult.Errors
                };
            }

            var userDTO = new UserDTO(user.Id, user.Name, user.Level, user.Faction, PasswordHasher.HashPassword(user.PasswordHash), user.Role);

            try
            {
                bool success = _userRepository.Add(userDTO);
           
               if (_userRepository.GetByName(userDTO.Username) != null) 
                  {
                    return new RegisterUserDTO
                    {
                        Success = success,
                        Errors = new List<string> { "A user with this name already exist" }
                    };
                    }
            
                return new RegisterUserDTO
                {
                    Success = success,
                    Errors = success ? new List<string>() : new List<string> { "Failed to add user to database." }
                };
            }
            catch (Exception ex)
            {
                return new RegisterUserDTO
                {
                    Success = false,
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }

}
