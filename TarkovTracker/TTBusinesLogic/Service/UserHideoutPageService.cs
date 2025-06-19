using BaseObjects.BaseObject;

namespace TarkovTrackerBLL.Service
{
    public class UserHideoutPageService
    {
        private readonly UserHideoutService _userHideoutService;
        private readonly HideoutStationService _hideoutStationService;

        public UserHideoutPageService(
            UserHideoutService userHideoutService,
            HideoutStationService hideoutStationService)
        {
            _userHideoutService = userHideoutService;
            _hideoutStationService = hideoutStationService;
        }

        public List<UserHideout> GetUserHideouts(int userId)
        {
            return _userHideoutService.GetAllUserHideouts(userId);
        }

        public List<HideoutStation> GetAvailableStations(int userId)
        {
            var allStations = _hideoutStationService.GetAllStations();
            var userHideouts = _userHideoutService.GetAllUserHideouts(userId);
            //gets all stations that the user doesnt have yet
            return allStations.Where(station => 
                !userHideouts.Any(uh => uh.StationId == station.Id)).ToList();
        }

        public bool UpgradeStation(int userId, int stationId)
        {
            return _userHideoutService.UpgradeStation(userId, stationId);
        }

        public bool AddStation(int userId, int stationId)
        {
            var userHideout = new UserHideout(userId, stationId, 1);
            return _userHideoutService.Add(userHideout);
        }

        public bool CanUpgrade(int userId, int stationId)
        {
            return _userHideoutService.CanUpgrade(userId, stationId);
        }
    }
} 