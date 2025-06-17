using BaseObjects.BaseObject;

namespace TarkovTrackerDAL.Interfaces
{
    public interface IUserHideoutRepository
    {
        List<UserHideout> GetAll(int userId);
        UserHideout GetByStationId(int userId, int stationId);
        bool Add(UserHideout userHideout);
        bool Remove(int userId, int stationId);
        bool Update(UserHideout userHideout);
    }
} 