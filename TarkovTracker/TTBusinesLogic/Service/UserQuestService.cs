using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseObjects.BaseObject;
using static TarkovTrackerBLL.Service.UserQuestService;

using TarkovTrackerDAL.Interfaces;

namespace TarkovTrackerBLL.Service
{
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
}