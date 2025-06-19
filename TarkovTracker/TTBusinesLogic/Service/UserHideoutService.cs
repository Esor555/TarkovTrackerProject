using BaseObjects.BaseObject;
using TarkovTrackerDAL.Interfaces;
using System;

namespace TarkovTrackerBLL.Service
{
    public class UserHideoutService
    {
        private const int MaxStationLevel = 3;
        private readonly IUserHideoutRepository _repository;

        public UserHideoutService(IUserHideoutRepository repository)
        {
            _repository = repository;
        }

        public List<UserHideout> GetAllUserHideouts(int userId)
        {
            return _repository.GetAll(userId);
        }

        public UserHideout GetUserHideoutByStationId(int userId, int stationId)
        {
            return _repository.GetByStationId(userId, stationId);
        }

        public bool Add(UserHideout userHideout)
        {
            if (userHideout.StationLevel < 0 || userHideout.StationLevel > MaxStationLevel)
                return false;
            return _repository.Add(userHideout);
        }

        public bool Remove(int userId, int stationId)
        {
            return _repository.Remove(userId, stationId);
        }

        public bool Update(UserHideout userHideout)
        {
            if (userHideout.StationLevel < 0 || userHideout.StationLevel > MaxStationLevel)
                return false;
            return _repository.Update(userHideout);
        }

        public bool UpgradeStation(int userId, int stationId)
        {
            var userHideout = _repository.GetByStationId(userId, stationId);
            if (userHideout == null)
            {
                // If station doesn't exist for user, create it at level 1
                userHideout = new UserHideout(userId, stationId, 1);
                return _repository.Add(userHideout);
            }

            // Check if already at max level
            if (userHideout.StationLevel >= MaxStationLevel)
                return false;

            // Increment the station level
            userHideout.StationLevel++;
            return _repository.Update(userHideout);
        }

        public bool CanUpgrade(int userId, int stationId)
        {
            var userHideout = _repository.GetByStationId(userId, stationId);
            return userHideout == null || userHideout.StationLevel < MaxStationLevel;
        }
    }
} 