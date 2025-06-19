using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseObjects.BaseObject;
using BaseObjects.DTO;
using TarkovTrackerBLL.DTO;
using TarkovTrackerBLL.Validators;
using TarkovTrackerDAL.Interfaces;
using TarkovTrackerDAL.Services;
using TarkovTrackerDAL.test;
using UserDTO = BaseObjects.DTO.UserDTO;


namespace TarkovTrackerBLL.Service
{
    public class UserService
    {
        private readonly IuserRepository _userRepository;

        public UserService(IuserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

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
                throw new Exception($"Error retrieving user with ID {username}", ex);
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
                throw new Exception("Error retrieving users", ex);
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
                throw new Exception($"Error retrieving user with ID {id}", ex);
            }
        }

        public bool AddUser(User user)
        {
            var validator = new UserValidator();
            var validationResult = validator.Validate(user);
            if (!validationResult.IsValid)
                throw new ArgumentException(string.Join("; ", validationResult.Errors));

            UserDTO userDTO = new UserDTO(user.Id, user.Name, user.Level, user.Faction, PasswordHasher.HashPassword(user.PasswordHash), user.Role);
            try
            {
                return _userRepository.Add(userDTO);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user", ex);
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
                throw new Exception($"Error deleting user with ID {id}", ex);
            }
        }

        public bool UpdateUser(User user)
        {
            var validator = new UserValidator();
            var validationResult = validator.Validate(user);
            if (!validationResult.IsValid)
                throw new ArgumentException(string.Join("; ", validationResult.Errors));

            UserDTO userDTO = new UserDTO(user.Id, user.Name, user.Level, user.Faction, PasswordHasher.HashPassword(user.PasswordHash), user.Role);
            if (user.Id <= 0)
                throw new ArgumentException("Invalid user ID");
            try
            {
                return _userRepository.Update(userDTO);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user", ex);
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
                if (_userRepository.GetByName(userDTO.Username) != null)
                {
                   
                    return new RegisterUserDTO
                    {
                        Success = false,
                        Errors = new List<string> { "A user with this name already exist" }
                    };
                }

                bool success = _userRepository.Add(userDTO); 

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
