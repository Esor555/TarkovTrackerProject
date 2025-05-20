using BaseObjects.ennums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseObjects.DTO
{
	public  class UserDTO
	{
		public int? Id { get; set; }
		public string Username { get; set; }
		public int? Level { get; set; }
		public Faction? Faction { get; set; }
		public string passwordhash { get; set; }
		public string? role { get; set; }

		public UserDTO()
		{

		}
		public UserDTO(int? id, string username, int? level, Faction? faction, string passwordhash, string? role)
		{
			if(id != null)
			this.Id = id;
			else 
			this.Id = null;
			this.Username = username;
			if(level != null) this.Level = level;
			else
			{
				this.Level = 1;
			}
			if (faction != null)
				this.Faction = faction;
			else
				this.Faction = ennums.Faction.USEC;
			this.passwordhash = passwordhash;
			if(role != null)
			this.role = role;
			else 
				this.role = "user";
		}
	}
}
