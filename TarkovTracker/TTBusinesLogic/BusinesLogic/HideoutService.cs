using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using TTBusinesLogic.BusinesLogic;
using TTBusinesLogic.BusinesLogic;
using TTBusinesLogic;

namespace YourApp.Services
{
    public class HideoutService
    {
        private readonly IWebHostEnvironment _env;

        public HideoutService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<Hideout> GetStations()
        {
            var path = Path.Combine(_env.WebRootPath, "json", "Hideout.json");

            if (!File.Exists(path))
                return new List<Hideout>();

            var json = File.ReadAllText(path);
            var root = JsonConvert.DeserializeObject<HideoutRoot>(json);
            return root?.Data?.HideoutStations ?? new List<Hideout>();
        }
    }
}
