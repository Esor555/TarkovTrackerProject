using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TTBusinesLogic.DAL;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace TarkovTracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration Configuration;
        public string Test {get; set; }
        public string TarkovTest { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
    
		}
		public void LoginTest()
		{
			


		}
		public void OnGet()
        {

         LoginService logine = new LoginService(Configuration);
		
			LoginSend login = new LoginSend("admin", "password");

			logine.Authenticate(login);
			LoginGet loginget = logine.Authenticate(login);
			

		}
       


    }
}
