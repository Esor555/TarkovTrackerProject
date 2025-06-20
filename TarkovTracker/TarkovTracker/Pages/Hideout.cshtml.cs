using BaseObjects.BaseObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TarkovTrackerBLL.DTO;
using TarkovTrackerBLL.Service;
using TarkovTrackerBLL.Service;


namespace TarkovTracker.Pages.Hideout
{
    public class IndexModel : PageModel
    {
        private readonly HideoutStationService _hideoutService;

        public IndexModel(IConfiguration config)
        {
            string connStr = config.GetConnectionString("1");
            _hideoutService = new HideoutStationService(connStr);
        }

        public List<HideoutStation> HideoutStations { get; set; }

        [BindProperty]
        public HideoutDTO HideoutStation { get; set; }

        public ActionResult OnGet()
			{
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }
            if (!User.IsInRole("admin"))
            {
                return RedirectToPage("/Index");
            }
       HideoutStations = _hideoutService.GetAllStations();
            return Page();
            
		}

        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(HideoutStation.name))
            {
                OnGet();
                return Page();
            }

            HideoutStation hideout = new HideoutStation(null, HideoutStation.name);
            _hideoutService.AddStation(hideout);
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            _hideoutService.DeleteStation(id);
            return RedirectToPage();
        }
    }
}