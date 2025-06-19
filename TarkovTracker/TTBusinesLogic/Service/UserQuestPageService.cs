using BaseObjects.BaseObject;
using System.Collections.Generic;
using System.Linq;

namespace TarkovTrackerBLL.Service
{
    public class UserQuestPageService
    {
        private readonly UserQuestService _userQuestService;
        private readonly QuestService _questService;

        public UserQuestPageService(UserQuestService userQuestService)
        {
            _userQuestService = userQuestService;
        }

        public UserQuestPageService(UserQuestService userQuestService, QuestService questService)
        {
            _userQuestService = userQuestService;
            _questService = questService;
        }

        public List<UserQuest> GetUserQuests(int userId)
        {
            return _userQuestService.GetAllUserQuests(userId);
        }

        public bool UpdateQuestStatus(int userId, int questId, string newStatus)
        {
            return _userQuestService.Update(new UserQuest
            {
                UserId = userId,
                QuestId = questId,
                Status = newStatus
            });
        }

        public bool AddQuest(int userId, int questId)
        {
            var userQuest = new UserQuest(userId, questId, "InProgress");
            return _userQuestService.Add(userQuest);
        }

        public bool CompleteQuest(int userId, int questId)
        {
            var userQuests = _userQuestService.GetAllUserQuests(userId);
            var userQuest = userQuests.FirstOrDefault(uq => uq.QuestId == questId);
            
            if (userQuest != null)
            {
                userQuest.Status = "Completed";
                return _userQuestService.Update(userQuest);
            }
            
            return false;
        }

        public List<Quest> GetAvailableQuests(int userId)
        {
            if (_questService == null) return new List<Quest>();
            
            var allQuests = _questService.GetAllQuests();
            var userQuests = _userQuestService.GetAllUserQuests(userId);
            var userQuestIds = userQuests.Select(uq => uq.QuestId).ToList();
            
            return allQuests.Where(q => !userQuestIds.Contains(q.Id)).ToList();
        }

        public List<Quest> GetQuestsByLevel(int level)
        {
            if (_questService == null) return new List<Quest>();
            
            var allQuests = _questService.GetAllQuests();
            return allQuests.Where(q => q.RequiredLevel == level).ToList();
        }
    }
}