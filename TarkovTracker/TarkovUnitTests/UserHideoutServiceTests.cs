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
    public class UserHideoutServiceTests
    {
        private Mock<IUserHideoutRepository> _mockUserHideoutRepository;
        private UserHideoutService _userHideoutService;
        private UserHideout _testUserHideout;

        [TestInitialize]
        public void Setup()
        {
            _mockUserHideoutRepository = new Mock<IUserHideoutRepository>();
            _userHideoutService = new UserHideoutService(_mockUserHideoutRepository.Object);
            _testUserHideout = new UserHideout(1, 1, 1);
        }

        [TestMethod]
        public void GetAllUserHideouts_ValidUserId_ReturnsUserHideouts()
        {
            var expectedUserHideouts = new List<UserHideout>
            {
                new UserHideout(1, 1, 1),
                new UserHideout(1, 2, 2)
            };
            _mockUserHideoutRepository.Setup(x => x.GetAll(1)).Returns(expectedUserHideouts);
            var result = _userHideoutService.GetAllUserHideouts(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetAllUserHideouts_NoUserHideouts_ReturnsEmptyList()
        {
            _mockUserHideoutRepository.Setup(x => x.GetAll(1)).Returns(new List<UserHideout>());
            var result = _userHideoutService.GetAllUserHideouts(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetUserHideoutByStationId_ValidIds_ReturnsUserHideout()
        {
            _mockUserHideoutRepository.Setup(x => x.GetByStationId(1, 1)).Returns(_testUserHideout);
            var result = _userHideoutService.GetUserHideoutByStationId(1, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(_testUserHideout.UserId, result.UserId);
        }

        [TestMethod]
        public void GetUserHideoutByStationId_NotExists_ReturnsNull()
        {
            _mockUserHideoutRepository.Setup(x => x.GetByStationId(1, 999)).Returns((UserHideout)null);
            var result = _userHideoutService.GetUserHideoutByStationId(1, 999);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_ValidUserHideout_ReturnsTrue()
        {
            _mockUserHideoutRepository.Setup(x => x.Add(_testUserHideout)).Returns(true);
            var result = _userHideoutService.Add(_testUserHideout);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Add_InvalidStationLevel_ReturnsFalse()
        {
            var invalidUserHideout = new UserHideout(1, 1, 4); // Level 4 is invalid (max is 3)
            var result = _userHideoutService.Add(invalidUserHideout);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Add_InvalidUserHideout_ReturnsFalse()
        {
            var invalidUserHideout = new UserHideout(1, -1, 1);
            var result = _userHideoutService.Add(invalidUserHideout);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Add_NegativeStationLevel_ReturnsFalse()
        {
            var invalidUserHideout = new UserHideout(1, 1, -1);
            var result = _userHideoutService.Add(invalidUserHideout);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Remove_ValidIds_ReturnsTrue()
        {
            _mockUserHideoutRepository.Setup(x => x.Remove(1, 1)).Returns(true);
            var result = _userHideoutService.Remove(1, 1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Remove_RepositoryReturnsFalse_ReturnsFalse()
        {
            _mockUserHideoutRepository.Setup(x => x.Remove(1, 1)).Returns(false);
            var result = _userHideoutService.Remove(1, 1);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Update_ValidUserHideout_ReturnsTrue()
        {
            _mockUserHideoutRepository.Setup(x => x.Update(_testUserHideout)).Returns(true);
            var result = _userHideoutService.Update(_testUserHideout);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Update_InvalidUserHideout_ReturnsFalse()
        {
            var invalidUserHideout = new UserHideout(1, -1, 1);
            var result = _userHideoutService.Update(invalidUserHideout);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpgradeStation_StationDoesNotExist_CreatesNewStation()
        {
            _mockUserHideoutRepository.Setup(x => x.GetByStationId(1, 1)).Returns((UserHideout)null);
            _mockUserHideoutRepository.Setup(x => x.Add(It.IsAny<UserHideout>())).Returns(true);
            var result = _userHideoutService.UpgradeStation(1, 1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpgradeStation_StationExistsBelowMaxLevel_UpgradesStation()
        {
            var userHideout = new UserHideout(1, 1, 1);
            _mockUserHideoutRepository.Setup(x => x.GetByStationId(1, 1)).Returns(userHideout);
            _mockUserHideoutRepository.Setup(x => x.Update(It.IsAny<UserHideout>())).Returns(true);
            var result = _userHideoutService.UpgradeStation(1, 1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpgradeStation_StationAtMaxLevel_ReturnsFalse()
        {
            var userHideout = new UserHideout(1, 1, 3);
            _mockUserHideoutRepository.Setup(x => x.GetByStationId(1, 1)).Returns(userHideout);
            var result = _userHideoutService.UpgradeStation(1, 1);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanUpgrade_StationDoesNotExist_ReturnsTrue()
        {
            _mockUserHideoutRepository.Setup(x => x.GetByStationId(1, 1)).Returns((UserHideout)null);
            var result = _userHideoutService.CanUpgrade(1, 1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanUpgrade_StationBelowMaxLevel_ReturnsTrue()
        {
            var userHideout = new UserHideout(1, 1, 2);
            _mockUserHideoutRepository.Setup(x => x.GetByStationId(1, 1)).Returns(userHideout);
            var result = _userHideoutService.CanUpgrade(1, 1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanUpgrade_StationAtMaxLevel_ReturnsFalse()
        {
            var userHideout = new UserHideout(1, 1, 3);
            _mockUserHideoutRepository.Setup(x => x.GetByStationId(1, 1)).Returns(userHideout);
            var result = _userHideoutService.CanUpgrade(1, 1);
            Assert.IsFalse(result);
        }
    }
} 