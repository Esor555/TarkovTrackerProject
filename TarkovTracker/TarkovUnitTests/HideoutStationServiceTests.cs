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
    public class HideoutStationServiceTests
    {
        private Mock<IhideoutstationRepository> _mockHideoutStationRepository;
        private HideoutStationService _hideoutStationService;
        private List<HideoutStation> _testStations;

        [TestInitialize]
        public void Setup()
        {
            _mockHideoutStationRepository = new Mock<IhideoutstationRepository>();
            _hideoutStationService = new HideoutStationService(_mockHideoutStationRepository.Object);
            _testStations = new List<HideoutStation>
            {
                new HideoutStation(1, "Workbench"),
                new HideoutStation(2, "Medstation")
            };
        }

        [TestMethod]
        public void GetAllStations_ReturnsAllStations()
        {
            _mockHideoutStationRepository.Setup(x => x.GetAll()).Returns(_testStations);
            var result = _hideoutStationService.GetAllStations();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetStationById_ValidId_ReturnsStation()
        {
            var expectedStation = _testStations[0];
            _mockHideoutStationRepository.Setup(x => x.GetById(1)).Returns(expectedStation);
            var result = _hideoutStationService.GetStationById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStation.Id, result.Id);
        }

        [TestMethod]
        public void AddStation_ValidStation_ReturnsTrue()
        {
            var station = _testStations[0];
            _mockHideoutStationRepository.Setup(x => x.Add(station)).Returns(true);
            var result = _hideoutStationService.AddStation(station);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateStation_ValidStation_ReturnsTrue()
        {
            var station = _testStations[0];
            _mockHideoutStationRepository.Setup(x => x.Update(station)).Returns(true);
            var result = _hideoutStationService.UpdateStation(station);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteStation_ValidId_ReturnsTrue()
        {
            _mockHideoutStationRepository.Setup(x => x.Delete(1)).Returns(true);
            var result = _hideoutStationService.DeleteStation(1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetStationsByName_ValidName_ReturnsMatchingStations()
        {
            _mockHideoutStationRepository.Setup(x => x.GetAll()).Returns(_testStations);
            var result = _hideoutStationService.GetStationsByName("work");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Workbench", result[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddStation_InvalidStation_ThrowsArgumentException()
        {
            var invalidStation = new HideoutStation(-1, "");
            _hideoutStationService.AddStation(invalidStation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateStation_InvalidStation_ThrowsArgumentException()
        {
            var invalidStation = new HideoutStation(-1, "");
            _hideoutStationService.UpdateStation(invalidStation);
        }
    }
} 