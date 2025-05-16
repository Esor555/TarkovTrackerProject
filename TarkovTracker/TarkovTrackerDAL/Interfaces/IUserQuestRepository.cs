using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseObjects.BaseObject;


namespace TarkovTrackerDAL.Interfaces
{
    public interface IUserQuestRepository
    {
        List<UserQuest> getall(int userId);
        bool Add(UserQuest userQuest);
        bool Remove(int userId, int questId);
        bool Update(UserQuest userQuest);
    }
}
