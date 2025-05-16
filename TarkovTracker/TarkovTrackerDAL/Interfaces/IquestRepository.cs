using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseObjects.BaseObject;

namespace TarkovTrackerDAL.Interfaces
{
    public interface IquestRepository
    {
        List<Quest> GetAll();
        Quest GetByName(string Quest);
        Quest GetById(int id);
        bool Add(Quest quest);
        bool Delete(int id);
        bool Update(Quest quest);

    }
}
