using Microsoft.Extensions.Configuration;
using TTBusinesLogic.BusinesLogic;
using TTBusinesLogic.DAL;

namespace tarkovTest
{
    [TestClass]
    public sealed class Test1
    {

	  
        [TestMethod]
        public void UserAdd()
        {
            UserRepository repository =
                new UserRepository(
                    "Data Source=mssqlstud.fhict.local;Database=dbi550462;TrustServerCertificate=True;Integrated Security=false;User ID=dbi550462;Password=Tarkov123!");
            Equals(true, repository.Add(new User(6, "stijn", 15, Faction.USAC)));

        }

       
    
    }
}
