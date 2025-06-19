using BaseObjects.BaseObject;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.Interfaces;

public class UserQuestService
{
    public IUserQuestRepository _IuserQuestRepository;

    public UserQuestService(IUserQuestRepository IuserQuestRepository)
    {
        _IuserQuestRepository = IuserQuestRepository;
    }

    public bool Add(UserQuest userQuest)
    {
        var validator = new TarkovTrackerBLL.Validators.UserQuestValidator();
        var validationResult = validator.Validate(userQuest);
        if (!validationResult.IsValid)
            throw new ArgumentException(string.Join("; ", validationResult.Errors));
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
        var validator = new TarkovTrackerBLL.Validators.UserQuestValidator();
        var validationResult = validator.Validate(userQuest);
        if (!validationResult.IsValid)
            throw new ArgumentException(string.Join("; ", validationResult.Errors));
        return _IuserQuestRepository.Update(userQuest);
    }
}