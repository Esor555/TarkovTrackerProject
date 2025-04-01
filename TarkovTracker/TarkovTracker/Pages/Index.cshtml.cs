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
      
        public void OnGet()
        {
            BaseDAL baseDAL = new BaseDAL(Configuration["ConnectionStrings:1"]);
            baseDAL.Connection();
            Test = baseDAL.test;
			TarkovApi();

		}
        public async Task TarkovApi()
        {
            var data = new Dictionary<string, string>()
            {
                {"query", "{maps{name}}"}
            };
            using (var httpClient = new HttpClient())
            {

                //Http response message
                var httpResponse = await httpClient.PostAsJsonAsync("https://api.tarkov.dev/graphql", data);

                //Response content
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                //_logger.LogInformation(responseContent);
                //Print response
                foreach (var test in responseContent)
                {
	                TarkovTest += test.ToString();
                }
                _logger.LogInformation(TarkovTest);

            }
        }


    }
}
