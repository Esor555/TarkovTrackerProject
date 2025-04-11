using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TTBusinesLogic;
using YourApp.Services;

namespace TarkovTracker.Pages
{
    public class hideoutModel : PageModel
    {
        private readonly HideoutService _hideoutService;
        public List<Hideout> Stations { get; set; }


        public hideoutModel(HideoutService hideoutService)
        {
            _hideoutService = hideoutService;
        }
        public void OnGet()
        {
            Stations = _hideoutService.GetStations();
        }
    }
}
