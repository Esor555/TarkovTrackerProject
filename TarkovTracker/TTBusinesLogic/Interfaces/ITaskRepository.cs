using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic.BusinesLogic;
using Task = TTBusinesLogic.BusinesLogic.Task;

namespace TTBusinesLogic.Interfaces
{
	public interface ITaskRepository
	{
		public List<Task> GetAll();
		public void Add(Task task);
		public Task GetById(int id);
		public void Delete(int id);
	}
}
