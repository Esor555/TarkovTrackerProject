using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TTBusinesLogic.Interfaces
{
    public interface IService<T> where T : class
    {
        public List<T> GetAll();
        public void Add(T var);
        public void Remove(int id);
    }
}
