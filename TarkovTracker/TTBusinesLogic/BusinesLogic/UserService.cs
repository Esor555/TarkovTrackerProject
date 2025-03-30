using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic.DAL;
namespace TTBusinesLogic.BusinesLogic
{
    public class UserService
    {
        private UserRepository repository;
        private UserValidator validator;
        public List<User> GetAll() { return new List<User>; }
        public void AddUser(User user) { }
        public User GetUserByid(int id) { return new User(); }
        public void DeleteUser(int id) { }
    }
}
