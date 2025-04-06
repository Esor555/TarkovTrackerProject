using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTBusinesLogic.BusinesLogic
{
    public class ItemRequirement
    {
        public int Count { get; set; }
        public string Id { get; set; }
        public int Quantity { get; set; }
        public Item Item { get; set; }

    }
}
