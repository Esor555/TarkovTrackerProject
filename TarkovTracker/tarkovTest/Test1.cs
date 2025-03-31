using TTBusinesLogic.BusinesLogic;

namespace tarkovTest
{
    [TestClass]
    public sealed class Test1
    {
        
        
        [TestMethod]
        public void UserAdd()
        {
            UserService service = new UserService(); 
            service.Add(new User(1,"stijn", 15, Faction.USAC));
        }
    }
}
