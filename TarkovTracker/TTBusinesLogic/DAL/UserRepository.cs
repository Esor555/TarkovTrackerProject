using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic.BusinesLogic;

namespace TTBusinesLogic.DAL
{
    public  class UserRepository
    {
        private List<User> Users;

        public List<User> GetAll() {
            //temporary code later used for database 
            return Users; 
        }
        public void AddUser(User user) 
        {
            //temporary code later used for database 
            Users.Add(user);
        }
        public User GetUserByid(int id) 
        {
            //temporary code later used for database 
            foreach (User user in Users)
            {
                if (user.Id == id) return user;
            }
            throw new Exception("something went wrong while looking for the id");
        }
        public void DeleteUser(int id) 
        {
            //temporary code later used for database 
            foreach(User user in Users)
            {
                if (user.Id == id)
                { 
                   Users.Remove(user);
                }    
            }
        }
    }
}
