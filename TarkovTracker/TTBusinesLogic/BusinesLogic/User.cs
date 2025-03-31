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
        public int Id {  get { return id; } set { id = value; } }
        private string name;
        public string Name { get { return name; } set { name = value; } }
        private int level;
        public int Level { get { return level; } set { level = value; } }
        private Faction faction;
        public Faction Faction { get { return faction; } set { faction = value; } }

        public User(int id, string name, int level, Faction faction)
        {
            this.Id = id;
            this.Name = name;
            this.Level = level;
            this.Faction = faction;
        }
        public User() { }
    }
}
