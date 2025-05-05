using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic.BusinesLogic;

namespace TTBusinesLogic.Interfaces
{
	public interface IuserRepository
	{
        List<UserDTO> GetAll();
        UserDTO GetById(int id);
        void Add(UserDTO user);
        void Update(UserDTO user);
        void Delete(int id);

    }
}
