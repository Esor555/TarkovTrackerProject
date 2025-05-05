using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TTBusinesLogic.BusinesLogic;
using TTBusinesLogic.DTO;
using TTBusinesLogic.Interfaces;

namespace TTBusinesLogic.DAL
{
    public  class UserRepository: BaseDAL, IuserRepository
    {
        private List<User> Users;
        public UserRepository(string ConnectionString) : base(ConnectionString) { }

        public void Add(UserDTO user)
        {
            using (var conn = CreateConnection())
            {
                string query = @"INSERT INTO user_data (username, level, faction)
                                 VALUES (@username, @level, @faction)";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@level", user.Level);
                cmd.Parameters.AddWithValue("@faction", user.Faction.ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = CreateConnection())
            {
                string query = "DELETE FROM user_data WHERE id = @id";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<UserDTO> GetAll()
        {
            var users = new List<UserDTO>();
            using (var conn = CreateConnection())
            {
                string query = "SELECT id, username, level, faction FROM user_data";
                var cmd = new SqlCommand(query, conn);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(MapToDTO(reader));
                    }
                }
            }
            return users;
        }

        public void Update(UserDTO user)
        {
            using (var conn = CreateConnection())
            {
                string query = @"UPDATE user_data 
                         SET username = @username, 
                             level = @level, 
                             faction = @faction 
                         WHERE id = @id";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@level", user.Level);
                cmd.Parameters.AddWithValue("@faction", user.Faction.ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public UserDTO GetById(int id)
        {
            using (var conn = CreateConnection())
            {
                string query = "SELECT id, username, level, faction FROM user_data WHERE id = @id";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapToDTO(reader);
                    }
                }
            }
            return null;
        }
        private UserDTO MapToDTO(SqlDataReader reader)
        {
            return new UserDTO
            {
                Id = Convert.ToInt32(reader["id"]),
                Username = reader["username"].ToString(),
                Level = Convert.ToInt32(reader["level"]),
                Faction = Enum.TryParse(reader["faction"].ToString(), out Faction result)
                    ? result
                    : Faction.USAC 
            };
        }
    }
}
