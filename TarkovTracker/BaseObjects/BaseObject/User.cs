using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseObjects.ennums;


namespace BaseObjects.BaseObject
{
    public class User
    {
        private int id;
        public int Id { get => id; set => id = value; }

        private string name;
        public string Name { get => name; set => name = value; }

        private string passwordHash;
        public string PasswordHash { get => passwordHash; set => passwordHash = value; }

        private int level;
        public int Level { get => level; set => level = value; }

        private string role;
        public string Role { get => role; set => role = value; }

        private DateTime createdAt;
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }

        private Faction faction;
        public Faction Faction { get => faction; set => faction = value; }

        public User(int id, string name, string passwordHash, int level, string role, DateTime createdAt, Faction faction)
        {
            Id = id;
            Name = name;
            PasswordHash = passwordHash;
            Level = level;
            Role = role;
            CreatedAt = createdAt;
            Faction = faction;
        }
        public User(int id, string name, int level, string role, DateTime createdAt, Faction faction)
        {
            Id = id;
            Name = name;

            Level = level;
            Role = role;
            CreatedAt = createdAt;
            Faction = faction;
        }
        public User() { }
    }
}
