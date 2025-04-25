using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic.DAL;
using TTBusinesLogic.Interfaces;
namespace TTBusinesLogic.BusinesLogic
{
    public class UserService : IService<User>
    {
        private UserRepository repository;
        private UserValidator validator;
        public List<User> GetAll() 
        {
            return repository.GetAll();        
        }
        public void Add(User user) 
        {
            try
            {
                if (validator.ValidateUser(user))
                {
                    repository.Add(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void Remove(int id)
        {
        }

        public void UpdateName(User user, string userName) 
        {
            if (validator.ValidateName(userName))
            {
                user.Name = userName;
            }
        }
        public void UpdateLevel(User user, int level) 
        {
            if (validator.ValidateLevel(level))
            {
                user.Level = level;
            }
        }
        public void UpdateFaction(User user, Faction faction) 
        {
            if (validator.ValidateFaction(faction))
            {
                user.Faction = faction;
            }
        }

        
    }
}
