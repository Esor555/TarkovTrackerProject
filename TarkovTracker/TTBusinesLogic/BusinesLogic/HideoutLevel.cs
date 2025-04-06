using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TTBusinesLogic.BusinesLogic
{
    public class HideoutLevel 
    {

        public int Level { get; set; }
        public int ConstructionTime { get; set; } // In seconds
        public List<ItemRequirement> ItemRequirements { get; set; }

    }
}
