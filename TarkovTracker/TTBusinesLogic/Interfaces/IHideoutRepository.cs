using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic.BusinesLogic;

namespace TTBusinesLogic.Interfaces
{
	public interface IHideoutRepository
	{
		public List<Hideout> GetAll();
		public void Add(Hideout hideout);
		public Hideout GetById(int id);
		public void Delete(int id);
	}
}
