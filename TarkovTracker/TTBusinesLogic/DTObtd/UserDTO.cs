using BaseObjects.ennums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarkovTrackerBLL.DTObtd
{
	public  class UserDTO
	{
		public int? Id { get; set; }
		public string Username { get; set; }
		public int? Level { get; set; }

		public Faction? Faction { get; set; }
		public string passwordhash { get; set; }
		public string? role { get; set; }
	}
}
