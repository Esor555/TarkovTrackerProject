using System;
using System.Collections.Generic;
using System.Linq;
using BaseObjects.BaseObject;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.Interfaces;
using Xunit;

namespace TarkovTracker.Tests
{
    public class HideoutTests
    {
        private class MockHideoutStationRepository : IHideoutStationRepository
        {
            private List<HideoutStation> _stations;

            public MockHideoutStationRepository()
            {
                _stations = new List<HideoutStation>
                {
                    new HideoutStation(1, "Workbench"),
                    new HideoutStation(2, "Medstation"),
                    new HideoutStation(3, "Lavatory"),
                    new HideoutStation(4, "Water Collector"),
                    new HideoutStation(5, "Heating")
                };
            }

            public List<HideoutStation> GetAll()
            {
                return _stations;
            }

            public HideoutStation GetById(int id)
            {
                return _stations.FirstOrDefault(s => s.Id == id);
            }
        }

        private class MockUserHideoutRepository : IUserHideoutRepository
        {
            private List<UserHideout> _userHideouts;

            public MockUserHideoutRepository()
            {
                _userHideouts = new List<UserHideout>();
            }

            public List<UserHideout> GetAll(int userId)
            {
                return _userHideouts.Where(uh => uh.UserId == userId).ToList();
            }

            public UserHideout GetByStationId(int userId, int stationId)
            {
                return _userHideouts.FirstOrDefault(uh => 
                    uh.UserId == userId && uh.StationId == stationId);
            }

            public bool Add(UserHideout userHideout)
            {
                if (_userHideouts.Any(uh => uh.UserId == userHideout.UserId && uh.StationId == userHideout.StationId))
                {
                    return false;
                }

                _userHideouts.Add(userHideout);
                return true;
            }

            public bool Update(UserHideout userHideout)
            {
                var existingHideout = _userHideouts.FirstOrDefault(uh => 
                    uh.UserId == userHideout.UserId && uh.StationId == userHideout.StationId);

                if (existingHideout == null)
                {
                    return false;
                }

                existingHideout.StationLevel = userHideout.StationLevel;
                return true;
            }

            public bool Remove(int userId, int stationId)
            {
                var hideout = _userHideouts.FirstOrDefault(uh => 
                    uh.UserId == userId && uh.StationId == stationId);

                if (hideout == null)
                {
                    return false;
                }

                return _userHideouts.Remove(hideout);
            }
        }

        [Fact]
        public void GetUserHideouts_ShouldReturnEmptyList_WhenUserHasNoStations()
        {
            // Arrange
            var mockStationRepo = new MockHideoutStationRepository();
            var mockUserHideoutRepo = new MockUserHideoutRepository();
            var service = new UserHideoutPageService(
                new UserHideoutService(mockUserHideoutRepo),
                new HideoutStationService(mockStationRepo));

            // Act
            var result = service.GetUserHideouts(1);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAvailableStations_ShouldReturnAllStations_WhenUserHasNoStations()
        {
            // Arrange
            var mockStationRepo = new MockHideoutStationRepository();
            var mockUserHideoutRepo = new MockUserHideoutRepository();
            var service = new UserHideoutPageService(
                new UserHideoutService(mockUserHideoutRepo),
                new HideoutStationService(mockStationRepo));

            // Act
            var result = service.GetAvailableStations(1);

            // Assert
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void AddStation_ShouldAddNewStation_WhenStationDoesNotExist()
        {
            // Arrange
            var mockStationRepo = new MockHideoutStationRepository();
            var mockUserHideoutRepo = new MockUserHideoutRepository();
            var service = new UserHideoutPageService(
                new UserHideoutService(mockUserHideoutRepo),
                new HideoutStationService(mockStationRepo));

            // Act
            var result = service.AddStation(1, 1);

            // Assert
            Assert.True(result);
            var userHideouts = service.GetUserHideouts(1);
            Assert.Single(userHideouts);
            Assert.Equal(1, userHideouts[0].StationLevel);
        }

        [Fact]
        public void UpgradeStation_ShouldIncreaseLevel_WhenBelowMaxLevel()
        {
            // Arrange
            var mockStationRepo = new MockHideoutStationRepository();
            var mockUserHideoutRepo = new MockUserHideoutRepository();
            var service = new UserHideoutPageService(
                new UserHideoutService(mockUserHideoutRepo),
                new HideoutStationService(mockStationRepo));

            // Add initial station
            service.AddStation(1, 1);

            // Act
            var result = service.UpgradeStation(1, 1);

            // Assert
            Assert.True(result);
            var userHideouts = service.GetUserHideouts(1);
            Assert.Equal(2, userHideouts[0].StationLevel);
        }

        [Fact]
        public void CanUpgrade_ShouldReturnFalse_WhenAtMaxLevel()
        {
            // Arrange
            var mockStationRepo = new MockHideoutStationRepository();
            var mockUserHideoutRepo = new MockUserHideoutRepository();
            var service = new UserHideoutPageService(
                new UserHideoutService(mockUserHideoutRepo),
                new HideoutStationService(mockStationRepo));

            // Add initial station and upgrade to max level
            service.AddStation(1, 1);
            service.UpgradeStation(1, 1);
            service.UpgradeStation(1, 1);

            // Act
            var result = service.CanUpgrade(1, 1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpgradeStation_ShouldCreateNewStation_WhenStationDoesNotExist()
        {
            // Arrange
            var mockStationRepo = new MockHideoutStationRepository();
            var mockUserHideoutRepo = new MockUserHideoutRepository();
            var service = new UserHideoutPageService(
                new UserHideoutService(mockUserHideoutRepo),
                new HideoutStationService(mockStationRepo));

            // Act
            var result = service.UpgradeStation(1, 1);

            // Assert
            Assert.True(result);
            var userHideouts = service.GetUserHideouts(1);
            Assert.Single(userHideouts);
            Assert.Equal(1, userHideouts[0].StationLevel);
        }

        [Fact]
        public void UpgradeStation_ShouldReturnFalse_WhenAtMaxLevel()
        {
            // Arrange
            var mockStationRepo = new MockHideoutStationRepository();
            var mockUserHideoutRepo = new MockUserHideoutRepository();
            var service = new UserHideoutPageService(
                new UserHideoutService(mockUserHideoutRepo),
                new HideoutStationService(mockStationRepo));

            // Add initial station and upgrade to max level
            service.AddStation(1, 1);
            service.UpgradeStation(1, 1);
            service.UpgradeStation(1, 1);

            // Act
            var result = service.UpgradeStation(1, 1);

            // Assert
            Assert.False(result);
            var userHideouts = service.GetUserHideouts(1);
            Assert.Equal(3, userHideouts[0].StationLevel);
        }
    }
} 