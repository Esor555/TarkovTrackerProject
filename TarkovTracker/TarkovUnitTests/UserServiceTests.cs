using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BaseObjects.BaseObject;
using BaseObjects.DTO;
using BaseObjects.ennums;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.Interfaces;
using System;
using System.Collections.Generic;

namespace TarkovUnitTests
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IuserRepository> _mockUserRepository;
        private UserService _userService;
        private User _testUser;

        [TestInitialize]
        public void Setup()
        {
            _mockUserRepository = new Mock<IuserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
            _testUser = new User
            {
                Id = 1,
                Name = "TestUser",
                Level = 10,
                Faction = Faction.USEC,
                PasswordHash = "password123",
                Role = "user"
            };
        }

        [TestMethod]
        public void GetByName_ValidUsername_ReturnsUser()
        {
            _mockUserRepository.Setup(x => x.GetByName("TestUser")).Returns(_testUser);
            var result = _userService.GetByName("TestUser");
            Assert.IsNotNull(result);
            Assert.AreEqual(_testUser.Name, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetByName_NullUsername_ThrowsArgumentException()
        {
            _userService.GetByName(null);
        }

        [TestMethod]
        public void GetAllUsers_ReturnsAllUsers()
        {
            var expectedUsers = new List<User> { _testUser };
            _mockUserRepository.Setup(x => x.GetAll()).Returns(expectedUsers);
            var result = _userService.GetAllUsers();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetUserById_ValidId_ReturnsUser()
        {
            _mockUserRepository.Setup(x => x.GetById(1)).Returns(_testUser);
            var result = _userService.GetUserById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(_testUser.Id, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUserById_InvalidId_ThrowsArgumentException()
        {
            _userService.GetUserById(0);
        }

        [TestMethod]
        public void AddUser_ValidUser_ReturnsTrue()
        {
            _mockUserRepository.Setup(x => x.Add(It.IsAny<UserDTO>())).Returns(true);
            var result = _userService.AddUser(_testUser);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddUser_NullUsername_ThrowsArgumentException()
        {
            _testUser.Name = null;
            _userService.AddUser(_testUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddUser_EmptyPassword_ThrowsArgumentException()
        {
            _testUser.PasswordHash = "";
            _userService.AddUser(_testUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddUser_InvalidUser_ThrowsArgumentException()
        {
            var invalidUser = new User { Name = "", Level = -1, Faction = (Faction)999, PasswordHash = "123", Role = "" };
            _userService.AddUser(invalidUser);
        }

        [TestMethod]
        public void DeleteUser_ValidId_ReturnsTrue()
        {
            _mockUserRepository.Setup(x => x.Delete(1)).Returns(true);
            var result = _userService.DeleteUser(1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteUser_InvalidId_ThrowsArgumentException()
        {
            _userService.DeleteUser(0);
        }

        [TestMethod]
        public void UpdateUser_ValidUser_ReturnsTrue()
        {
            _mockUserRepository.Setup(x => x.Update(It.IsAny<UserDTO>())).Returns(true);
            var result = _userService.UpdateUser(_testUser);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUser_InvalidId_ThrowsArgumentException()
        {
            _testUser.Id = 0;
            _userService.UpdateUser(_testUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUser_InvalidUser_ThrowsArgumentException()
        {
            var invalidUser = new User { Id = 1, Name = "", Level = -1, Faction = (Faction)999, PasswordHash = "123", Role = "" };
            _userService.UpdateUser(invalidUser);
        }

        [TestMethod]
        public void RegisterUser_ValidUser_ReturnsSuccess()
        {
            _mockUserRepository.Setup(x => x.Add(It.IsAny<UserDTO>())).Returns(true);
            _mockUserRepository.Setup(x => x.GetByName(_testUser.Name)).Returns((User)null);
            var result = _userService.RegisterUser(_testUser);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.Errors.Count);
        }

        [TestMethod]
        public void RegisterUser_UserAlreadyExists_ReturnsFailure()
        {
            _mockUserRepository.Setup(x => x.Add(It.IsAny<UserDTO>())).Returns(true);
            _mockUserRepository.Setup(x => x.GetByName(_testUser.Name)).Returns(_testUser);
            var result = _userService.RegisterUser(_testUser);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Contains("A user with this name already exist"));
        }
    }
} 