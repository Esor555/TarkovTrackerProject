using System.Security.Claims;
using BaseObjects.BaseObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TarkovTrackerBLL.Service;

namespace TarkovTracker.Pages.User
{
    public class HideoutModel : PageModel
    {
        private readonly UserHideoutPageService _pageService;
        private readonly HideoutStationService _hideoutStationService;

        public List<UserHideout> UserHideouts { get; set; }
        public List<HideoutStation> AvailableStations { get; set; }
        public List<HideoutStation> AllStations { get; set; }
        public Dictionary<int, bool> CanUpgradeStations { get; set; }

        public HideoutModel(
            UserHideoutPageService pageService,
            HideoutStationService hideoutStationService)
        {
            _pageService = pageService;
            _hideoutStationService = hideoutStationService;
            CanUpgradeStations = new Dictionary<int, bool>();
        }

        public ActionResult OnGet()
        {
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToPage("/Login");
			}
			int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UserHideouts = _pageService.GetUserHideouts(userId);
            AvailableStations = _pageService.GetAvailableStations(userId);
            AllStations = _hideoutStationService.GetAllStations();


            foreach (var hideout in UserHideouts)
            {
                CanUpgradeStations[hideout.StationId] = _pageService.CanUpgrade(userId, hideout.StationId);
            }

            return Page();
        }

        public IActionResult OnPostAddStation(int stationId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _pageService.AddStation(userId, stationId);
            return RedirectToPage();
        }

        public IActionResult OnPostUpgrade(int stationId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _pageService.UpgradeStation(userId, stationId);
            return RedirectToPage();
        }
    }
} 