using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BaseObjects.BaseObject;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.Interfaces;
using System;
using System.Collections.Generic;

namespace TarkovUnitTests
{
    [TestClass]
    public class TraderServiceTests
    {
        private Mock<ITraderRepository> _mockTraderRepository;
        private TraderService _traderService;
        private List<Trader> _testTraders;

        [TestInitialize]
        public void Setup()
        {
            _mockTraderRepository = new Mock<ITraderRepository>();
            _traderService = new TraderService(_mockTraderRepository.Object);
            _testTraders = new List<Trader>
            {
                new Trader(1, "Prapor", "Russian trader"),
                new Trader(2, "Therapist", "Medical trader")
            };
        }

        [TestMethod]
        public void GetAllTraders_ReturnsAllTraders()
        {
            _mockTraderRepository.Setup(x => x.GetAll()).Returns(_testTraders);
            var result = _traderService.GetAllTraders();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetTraderById_ValidId_ReturnsTrader()
        {
            var expectedTrader = _testTraders[0];
            _mockTraderRepository.Setup(x => x.GetById(1)).Returns(expectedTrader);
            var result = _traderService.GetTraderById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTrader.Id, result.Id);
        }

        [TestMethod]
        public void AddTrader_ValidTrader_ReturnsTrue()
        {
            var trader = _testTraders[0];
            _mockTraderRepository.Setup(x => x.Add(trader)).Returns(true);
            var result = _traderService.AddTrader(trader);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateTrader_ValidTrader_ReturnsTrue()
        {
            var trader = _testTraders[0];
            _mockTraderRepository.Setup(x => x.Update(trader)).Returns(true);
            var result = _traderService.UpdateTrader(trader);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteTrader_ValidId_ReturnsTrue()
        {
            _mockTraderRepository.Setup(x => x.Delete(1)).Returns(true);
            var result = _traderService.DeleteTrader(1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetByName_ValidName_ReturnsTrader()
        {
            var expectedTrader = _testTraders[0];
            _mockTraderRepository.Setup(x => x.GetByName("Prapor")).Returns(expectedTrader);
            var result = _traderService.GetByName("Prapor");
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedTrader.Name, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetByName_EmptyName_ThrowsArgumentException()
        {
            _traderService.GetByName("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullTrader_ThrowsArgumentNullException()
        {
            _traderService.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_EmptyTraderName_ThrowsArgumentException()
        {
            var trader = new Trader(3, "", "No name");
            _traderService.Add(trader);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_InvalidId_ThrowsArgumentException()
        {
            _traderService.Delete(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_InvalidTraderId_ThrowsArgumentException()
        {
            var trader = new Trader(0, "Bad", "Bad");
            _traderService.Update(trader);
        }
    }
} 