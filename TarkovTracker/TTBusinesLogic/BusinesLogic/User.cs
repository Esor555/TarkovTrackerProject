using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TTBusinesLogic.BusinesLogic
{
    public class User
    {
        private int id;
        public int Id {  get { return id; } }
        private string name;
        public string Name { get { return name; } }
        private int level;
        public int Level { get { return level; } }
        private Faction faction;
        public Faction Faction { get { return faction; } }
    }
}
