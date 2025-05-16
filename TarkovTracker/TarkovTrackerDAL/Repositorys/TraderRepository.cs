using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseObjects.BaseObject;


namespace TarkovTrackerDAL.test
{
    public class TraderRepository : ITraderRepository
    {
        private readonly string _connectionString;

        public TraderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Trader> GetAll()
        {
            var traders = new List<Trader>();
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM trader";
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        traders.Add(new Trader
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            return traders;
        }

        public Trader GetByName(string traderName)
        {
            Trader trader = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM trader WHERE name = @name";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", traderName);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        trader = new Trader
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                    }
                }
            }
            return trader;
        }

        public Trader GetById(int id)
        {
            Trader trader = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM trader WHERE id = @id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        trader = new Trader()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                    }
                }
            }
            return trader;
        }

        public bool Add(Trader trader)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO trader (name) VALUES (@name)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", trader.Name);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM trader WHERE id = @id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(Trader trader)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE trader SET name = @name WHERE id = @id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", trader.Name);
                command.Parameters.AddWithValue("@id", trader.Id);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
