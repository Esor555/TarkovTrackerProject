using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseObjects.BaseObject;
using BaseObjects.DTO;


namespace TarkovTrackerDAL.Interfaces
{
    public interface IuserRepository
    {
        List<User> GetAll();
        User GetByName(string userName);
        User GetById(int id);
        bool Add(UserDTO user);
        bool Delete(int id);
        bool Update(UserDTO user);

    }
}
