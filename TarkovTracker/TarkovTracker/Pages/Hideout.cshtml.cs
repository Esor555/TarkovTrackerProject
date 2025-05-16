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

        public void OnGet()
        {
            HideoutStations = _hideoutService.GetAllStations();
  
        }

        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(HideoutStation.name))
            {
                OnGet();
                return Page();
            }

            HideoutStation hideout = new HideoutStation(HideoutStation.id, HideoutStation.name);
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