using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic.BusinesLogic;
using TTBusinesLogic.Interfaces;

namespace TTBusinesLogic.DAL
{
    public class HideoutStationRepository : IhideoutstationRepository
    {
        private readonly string _connectionString;

        public HideoutStationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<HideoutStation> GetAll()
        {
            var stations = new List<HideoutStation>();
            string query = "SELECT * FROM hideout_station";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    stations.Add(new HideoutStation(
                        id: (int)reader["id"],
                        name: (string)reader["name"]
                    ));
                }
            }

            return stations;
        }

        public HideoutStation GetByName(string hideoutName)
        {
            HideoutStation station = null;
            string query = "SELECT * FROM hideout_station WHERE name = @name";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", hideoutName);
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    station = new HideoutStation(
                        id: (int)reader["id"],
                        name: (string)reader["name"]
                    );
                }
            }

            return station;
        }

        public HideoutStation GetById(int id)
        {
            HideoutStation station = null;
            string query = "SELECT * FROM hideout_station WHERE id = @id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    station = new HideoutStation(
                        id: (int)reader["id"],
                        name: (string)reader["name"]
                    );
                }
            }

            return station;
        }

        public bool Add(HideoutStation hideoutStation)
        {
            string query = "INSERT INTO hideout_station (name) VALUES (@name)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", hideoutStation.Name);
                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(HideoutStation hideoutStation)
        {
            string query = "UPDATE hideout_station SET name = @name WHERE id = @id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", hideoutStation.Name);
                command.Parameters.AddWithValue("@id", hideoutStation.Id);
                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM hideout_station WHERE id = @id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
