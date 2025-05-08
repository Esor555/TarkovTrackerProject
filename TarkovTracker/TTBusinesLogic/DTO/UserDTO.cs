using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic.BusinesLogic;
using TTBusinesLogic.enums;

namespace TTBusinesLogic.DTO
{
    public class UserDTO
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public int? Level { get; set; }
       
        public Faction? Faction { get; set; }
        public string password { get; set; }
        public string? role { get; set; }
    }
}
