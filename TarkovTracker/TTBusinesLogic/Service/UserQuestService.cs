using BaseObjects.BaseObject;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.Interfaces;

public class UserQuestService : IUserQuestService
{
    public IUserQuestRepository _IuserQuestRepository;

    public UserQuestService(IUserQuestRepository IuserQuestRepository)
    {
        _IuserQuestRepository = IuserQuestRepository;
    }

    public bool Add(UserQuest userQuest)
    {
        return _IuserQuestRepository.Add(userQuest);
    }

    public List<UserQuest> GetAllUserQuests(int userId)
    {
        return _IuserQuestRepository.getall(userId);
    }

    public bool Remove(int userId, int questId)
    {
        return _IuserQuestRepository.Remove(userId, questId);
    }

    public bool Update(UserQuest userQuest)
    {
        return _IuserQuestRepository.Update(userQuest);
    }
}