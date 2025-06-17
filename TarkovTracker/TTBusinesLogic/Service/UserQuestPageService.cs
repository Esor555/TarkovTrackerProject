using BaseObjects.BaseObject;

namespace TarkovTrackerBLL.Service
{
    public class UserQuestPageService
    {
        private readonly UserQuestService _userQuestService;

        public UserQuestPageService(UserQuestService userQuestService)
        {
            _userQuestService = userQuestService;
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
    }
}