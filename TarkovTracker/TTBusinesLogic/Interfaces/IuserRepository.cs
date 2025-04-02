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
		public List<User> GetAll();
		public void Add(User user);
		public User GetById(int id);
		public void Delete(int id);
	}
}
