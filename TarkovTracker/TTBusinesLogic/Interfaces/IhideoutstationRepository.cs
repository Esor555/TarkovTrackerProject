using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic.BusinesLogic;

namespace TTBusinesLogic.Interfaces
{
    public interface IhideoutstationRepository
    {
        List<HideoutStation> GetAll();
        HideoutStation GetByName(string HideoutName);
        HideoutStation GetById(int id);
        bool Add(HideoutStation HideoutStation);
        bool Delete(int id);
        bool Update(HideoutStation quest);

    }
}