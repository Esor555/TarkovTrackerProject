// BLL/Service/UserQuestPageService.cs

using BaseObjects.BaseObject;
using TarkovTrackerDAL.Interfaces;

namespace TarkovTrackerBLL.Service
{
    public class UserQuestPageService
    {
        private readonly IUserQuestService _userQuestService;

        public UserQuestPageService(IUserQuestService userQuestService)
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