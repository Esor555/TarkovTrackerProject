using BaseObjects.BaseObject;

namespace TarkovTrackerBLL.Service
{
    public class QuestPageService
    {
        private readonly QuestService _questService;
        private readonly IUserQuestService _userQuestService;

        public QuestPageService(QuestService questService, IUserQuestService userQuestService)
        {
            _questService = questService;
            _userQuestService = userQuestService;
        }

        public List<Quest> GetAllQuests() => _questService.GetAllQuests();

        public List<UserQuest> GetUserInProgressQuests(int userId) =>
            _userQuestService.GetAllUserQuests(userId)
                .Where(q => q.Status == "InProgress")
                .ToList();

        public void AddQuest(Quest quest) => _questService.AddQuest(quest);

        public void DeleteQuest(int questId) => _questService.DeleteQuest(questId);
    }
}