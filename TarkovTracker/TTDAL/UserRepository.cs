using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic;

namespace TTDAL
{
    public  class UserRepository
    {
        private List<User> Users;

        public List<User> GetAll() {  return Users; }
        public void AddUser(User user) { }
        public User GetUserByid(int id) { return new User(); }
        public void DeleteUser(int id) { }
    }
}
