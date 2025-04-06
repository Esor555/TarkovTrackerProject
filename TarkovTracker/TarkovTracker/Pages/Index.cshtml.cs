using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TTBusinesLogic.DAL;
using TTBusinesLogic.BusinesLogic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TTBusinesLogic;
using Newtonsoft.Json;
using YourApp.Services;
namespace TarkovTracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration Configuration;
        private readonly IWebHostEnvironment _env;

        private readonly HideoutService _hideoutService;

        public List<Hideout> Stations { get; set; }


        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, HideoutService hideoutService)
        {
            _logger = logger;
            Configuration = configuration;
            _hideoutService = hideoutService;

        }
        public void LoginTest()
		{
			


		}
		public void OnGet()
        {
            Stations = _hideoutService.GetStations();


        }
       


    }
}
