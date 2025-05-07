using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTBusinesLogic.BusinesLogic
{
    public class Trader
    {
        private int id;
        public int Id { get => id; set => id = value; }

        private string name;
        public string Name { get => name; set => name = value; }

        private string description;
        public string Description { get => description; set => description = value; }

        public Trader(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Trader()
        {
        }
    }
}
