using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using TTBusinesLogic.BusinesLogic;
using TTBusinesLogic.DTO;
using TTBusinesLogic.enums;
using TTBusinesLogic.Interfaces;

namespace TTBusinesLogic.DAL
{
    public  class UserRepository: BaseDAL, IuserRepository
    {
        public UserRepository(string connectionString) : base(connectionString) { }

            public List<User> GetAll()
            {
                var users = new List<User>();
                string query = "SELECT id, username, level, faction, created_at, role FROM user_data";

                using (SqlConnection conn = CreateConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new User()
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Level = reader.GetInt32(2),
                                    Faction = (Faction)reader.GetInt32(3),
                                    CreatedAt = reader.GetDateTime(4),
                                    Role = reader.GetString(5)
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in GetAll: " + ex.Message);
                    }
                }

                return users;
            }



		public User GetByName(string userName)
		{
			User user = null;
			string query = "SELECT id, username, level, faction, password_hash, role FROM user_data WHERE username = @username";

			using (SqlConnection conn = CreateConnection())
			using (SqlCommand cmd = new SqlCommand(query, conn))
			{
				cmd.Parameters.AddWithValue("@username", userName);

				try
				{
					conn.Open();
					using (var reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							user = new User()
							{
								Id = reader.GetInt32(0),
								Name = reader.GetString(1),
								Level = reader.GetInt32(2),
								Faction = (Faction)reader.GetInt32(3),
								PasswordHash = reader.GetString(4),
                                Role = reader.GetString(5)
							};
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error in GetById: " + ex.Message);
				}
			}

			return user;
		}
		public User GetById(int id)
        {
	        User user = null;
            string query = "SELECT id, username, level, faction, password_hash, role FROM user_data WHERE id = @id";

            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Level = reader.GetInt32(2),
                                Faction = (Faction)reader.GetInt32(3),
                                PasswordHash = reader.GetString(4),
                                Role = reader.GetString(5)
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in GetById: " + ex.Message);
                }
            }

            return user;
        }

        public bool Add(User user)
        {
            string query = "INSERT INTO user_data (username, level, password_hash, role, faction) VALUES (@username, @level, @passwordhash, @role, @faction)";

            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", user.Name);
                cmd.Parameters.AddWithValue("@level", user.Level);
                cmd.Parameters.AddWithValue("@passwordhash", PasswordHasher.HashPassword(user.PasswordHash));
                cmd.Parameters.AddWithValue("@role", user.Role);
                cmd.Parameters.AddWithValue("@faction", user.Faction);

                try
                {
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Add: " + ex.Message);
                }

                return false;
            }
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM user_data WHERE id = @id";

            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Delete: " + ex.Message);
                }

                return false;
            }
        }

        public bool Update(User user)
        {
            string query = "UPDATE user_data SET username = @username, level = @level, faction = @faction WHERE id = @id";

            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", user.Name);
                cmd.Parameters.AddWithValue("@level", user.Level);
                cmd.Parameters.AddWithValue("@faction", user.Faction);
                cmd.Parameters.AddWithValue("@id", user.Id);

                try
                {
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Update: " + ex.Message);
                }
                return false;
            }
        }



   
    }
}
