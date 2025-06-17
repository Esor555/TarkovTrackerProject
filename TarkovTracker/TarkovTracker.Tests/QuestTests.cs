using System;
using System.Collections.Generic;
using System.Linq;
using BaseObjects.BaseObject;
using TarkovTrackerBLL.Service;
using Xunit;

namespace TarkovTracker.Tests
{
    public class QuestTests
    {
        private class MockQuestRepository
        {
            private List<Quest> _quests;

            public MockQuestRepository()
            {
                _quests = new List<Quest>
                {
                    new Quest(1, "Checking", "Check the first quest", 1),
                    new Quest(2, "Checking 2", "Check the second quest", 1),
                    new Quest(3, "Checking 3", "Check the third quest", 2),
                    new Quest(4, "Checking 4", "Check the fourth quest", 2),
                    new Quest(5, "Checking 5", "Check the fifth quest", 3)
                };
            }

            public List<Quest> GetAllQuests()
            {
                return _quests;
            }

            public Quest GetQuestById(int id)
            {
                return _quests.FirstOrDefault(q => q.Id == id);
            }
        }

        private class MockUserQuestRepository
        {
            private List<UserQuest> _userQuests;

            public MockUserQuestRepository()
            {
                _userQuests = new List<UserQuest>();
            }

            public List<UserQuest> GetAllUserQuests(int userId)
            {
                return _userQuests.Where(uq => uq.UserId == userId).ToList();
            }

            public bool Add(UserQuest userQuest)
            {
                if (_userQuests.Any(uq => uq.UserId == userQuest.UserId && uq.QuestId == userQuest.QuestId))
                {
                    return false;
                }

                _userQuests.Add(userQuest);
                return true;
            }

            public bool Update(UserQuest userQuest)
            {
                var existingQuest = _userQuests.FirstOrDefault(uq => 
                    uq.UserId == userQuest.UserId && uq.QuestId == userQuest.QuestId);

                if (existingQuest == null)
                {
                    return false;
                }

                existingQuest.Completed = userQuest.Completed;
                return true;
            }
        }

        [Fact]
        public void GetUserQuests_ShouldReturnEmptyList_WhenUserHasNoQuests()
        {
            // Arrange
            var mockQuestRepo = new MockQuestRepository();
            var mockUserQuestRepo = new MockUserQuestRepository();
            var service = new UserQuestPageService(
                new UserQuestService(mockUserQuestRepo),
                new QuestService(mockQuestRepo));

            // Act
            var result = service.GetUserQuests(1);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAvailableQuests_ShouldReturnAllQuests_WhenUserHasNoQuests()
        {
            // Arrange
            var mockQuestRepo = new MockQuestRepository();
            var mockUserQuestRepo = new MockUserQuestRepository();
            var service = new UserQuestPageService(
                new UserQuestService(mockUserQuestRepo),
                new QuestService(mockQuestRepo));

            // Act
            var result = service.GetAvailableQuests(1);

            // Assert
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void AddQuest_ShouldAddNewQuest_WhenQuestDoesNotExist()
        {
            // Arrange
            var mockQuestRepo = new MockQuestRepository();
            var mockUserQuestRepo = new MockUserQuestRepository();
            var service = new UserQuestPageService(
                new UserQuestService(mockUserQuestRepo),
                new QuestService(mockQuestRepo));

            // Act
            var result = service.AddQuest(1, 1);

            // Assert
            Assert.True(result);
            var userQuests = service.GetUserQuests(1);
            Assert.Single(userQuests);
            Assert.False(userQuests[0].Completed);
        }

        [Fact]
        public void CompleteQuest_ShouldMarkQuestAsCompleted()
        {
            // Arrange
            var mockQuestRepo = new MockQuestRepository();
            var mockUserQuestRepo = new MockUserQuestRepository();
            var service = new UserQuestPageService(
                new UserQuestService(mockUserQuestRepo),
                new QuestService(mockQuestRepo));

            // Add initial quest
            service.AddQuest(1, 1);

            // Act
            var result = service.CompleteQuest(1, 1);

            // Assert
            Assert.True(result);
            var userQuests = service.GetUserQuests(1);
            Assert.True(userQuests[0].Completed);
        }

        [Fact]
        public void GetQuestsByLevel_ShouldReturnCorrectQuests()
        {
            // Arrange
            var mockQuestRepo = new MockQuestRepository();
            var mockUserQuestRepo = new MockUserQuestRepository();
            var service = new UserQuestPageService(
                new UserQuestService(mockUserQuestRepo),
                new QuestService(mockQuestRepo));

            // Act
            var level1Quests = service.GetQuestsByLevel(1);
            var level2Quests = service.GetQuestsByLevel(2);
            var level3Quests = service.GetQuestsByLevel(3);

            // Assert
            Assert.Equal(2, level1Quests.Count);
            Assert.Equal(2, level2Quests.Count);
            Assert.Single(level3Quests);
        }
    }
} 