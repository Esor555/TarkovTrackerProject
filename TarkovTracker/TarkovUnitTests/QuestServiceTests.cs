using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BaseObjects.BaseObject;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TarkovUnitTests
{
    [TestClass]
    public class QuestServiceTests
    {
        private Mock<IquestRepository> _mockQuestRepository;
        private QuestService _questService;
        private List<Quest> _testQuests;

        [TestInitialize]
        public void Setup()
        {
            _mockQuestRepository = new Mock<IquestRepository>();
            _questService = new QuestService(_mockQuestRepository.Object);
            _testQuests = new List<Quest>
            {
                new Quest(1, "First Quest", "Complete the first quest", 1, null, 1, "wiki1"),
                new Quest(2, "Second Quest", "Complete the second quest", 2, 1, 1, "wiki2"),
                new Quest(3, "Third Quest", "Complete the third quest", 3, 2, 2, "wiki3")
            };
        }

        [TestMethod]
        public void GetAllQuests_ReturnsAllQuests()
        {
            _mockQuestRepository.Setup(x => x.GetAll()).Returns(_testQuests);
            var result = _questService.GetAllQuests();
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetQuestById_ValidId_ReturnsQuest()
        {
            var expectedQuest = _testQuests[0];
            _mockQuestRepository.Setup(x => x.GetById(1)).Returns(expectedQuest);
            var result = _questService.GetQuestById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedQuest.Id, result.Id);
        }

        [TestMethod]
        public void GetQuestByTitle_ValidTitle_ReturnsQuest()
        {
            var expectedQuest = _testQuests[0];
            _mockQuestRepository.Setup(x => x.GetByName("First Quest")).Returns(expectedQuest);
            var result = _questService.GetQuestByTitle("First Quest");
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedQuest.Title, result.Title);
        }

        [TestMethod]
        public void AddQuest_ValidQuest_ReturnsTrue()
        {
            var quest = _testQuests[0];
            _mockQuestRepository.Setup(x => x.Add(quest)).Returns(true);
            var result = _questService.AddQuest(quest);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddQuest_EmptyTitle_ThrowsArgumentException()
        {
            var quest = new Quest(1, "", "Description", 1, null, 1, "wiki");
            _questService.AddQuest(quest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddQuest_InvalidQuest_ThrowsArgumentException()
        {
            var invalidQuest = new Quest(1, "", "", -1, null, 1, "");
            _questService.AddQuest(invalidQuest);
        }

        [TestMethod]
        public void UpdateQuest_ValidQuest_ReturnsTrue()
        {
            var quest = _testQuests[0];
            _mockQuestRepository.Setup(x => x.Update(quest)).Returns(true);
            var result = _questService.UpdateQuest(quest);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateQuest_InvalidQuest_ThrowsArgumentException()
        {
            var invalidQuest = new Quest(1, "", "", -1, null, 1, "");
            _questService.UpdateQuest(invalidQuest);
        }

        [TestMethod]
        public void DeleteQuest_ValidId_ReturnsTrue()
        {
            _mockQuestRepository.Setup(x => x.Delete(1)).Returns(true);
            var result = _questService.DeleteQuest(1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetQuestsByPreviousQuestId_ValidId_ReturnsQuests()
        {
            _mockQuestRepository.Setup(x => x.GetAll()).Returns(_testQuests);
            var result = _questService.GetQuestsByPreviousQuestId(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, result[0].Id);
        }

        [TestMethod]
        public void GetAvailableStartingQuestsForUser_ValidUser_ReturnsAvailableQuests()
        {
            var userQuests = new List<UserQuest> { new UserQuest(1, 1, "InProgress") };
            _mockQuestRepository.Setup(x => x.GetAll()).Returns(_testQuests);
            var result = _questService.GetAvailableStartingQuestsForUser(1, 2, userQuests);
            Assert.IsNotNull(result);
        }
    }
} 