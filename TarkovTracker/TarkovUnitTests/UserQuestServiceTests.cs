using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BaseObjects.BaseObject;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.Interfaces;
using System.Collections.Generic;
using System;

namespace TarkovUnitTests
{
    [TestClass]
    public class UserQuestServiceTests
    {
        private Mock<IUserQuestRepository> _mockUserQuestRepository;
        private UserQuestService _userQuestService;
        private UserQuest _testUserQuest;

        [TestInitialize]
        public void Setup()
        {
            _mockUserQuestRepository = new Mock<IUserQuestRepository>();
            _userQuestService = new UserQuestService(_mockUserQuestRepository.Object);
            _testUserQuest = new UserQuest(1, 1, "In Progress");
        }

        [TestMethod]
        public void Add_ValidUserQuest_ReturnsTrue()
        {
            _mockUserQuestRepository.Setup(x => x.Add(_testUserQuest)).Returns(true);
            var result = _userQuestService.Add(_testUserQuest);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Add_RepositoryReturnsFalse_ReturnsFalse()
        {
            _mockUserQuestRepository.Setup(x => x.Add(_testUserQuest)).Returns(false);
            var result = _userQuestService.Add(_testUserQuest);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAllUserQuests_ValidUserId_ReturnsUserQuests()
        {
            var expectedUserQuests = new List<UserQuest>
            {
                new UserQuest(1, 1, "In Progress"),
                new UserQuest(1, 2, "Completed")
            };
            _mockUserQuestRepository.Setup(x => x.getall(1)).Returns(expectedUserQuests);
            var result = _userQuestService.GetAllUserQuests(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetAllUserQuests_NoUserQuests_ReturnsEmptyList()
        {
            _mockUserQuestRepository.Setup(x => x.getall(1)).Returns(new List<UserQuest>());
            var result = _userQuestService.GetAllUserQuests(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Remove_ValidUserAndQuestId_ReturnsTrue()
        {
            _mockUserQuestRepository.Setup(x => x.Remove(1, 1)).Returns(true);
            var result = _userQuestService.Remove(1, 1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Remove_RepositoryReturnsFalse_ReturnsFalse()
        {
            _mockUserQuestRepository.Setup(x => x.Remove(1, 1)).Returns(false);
            var result = _userQuestService.Remove(1, 1);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Update_ValidUserQuest_ReturnsTrue()
        {
            _mockUserQuestRepository.Setup(x => x.Update(_testUserQuest)).Returns(true);
            var result = _userQuestService.Update(_testUserQuest);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Update_RepositoryReturnsFalse_ReturnsFalse()
        {
            _mockUserQuestRepository.Setup(x => x.Update(_testUserQuest)).Returns(false);
            var result = _userQuestService.Update(_testUserQuest);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_InvalidUserQuest_ThrowsArgumentException()
        {
            var invalidUserQuest = new UserQuest(0, 0, "");
            _userQuestService.Add(invalidUserQuest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_InvalidUserQuest_ThrowsArgumentException()
        {
            var invalidUserQuest = new UserQuest(0, 0, "");
            _userQuestService.Update(invalidUserQuest);
        }
    }
} 