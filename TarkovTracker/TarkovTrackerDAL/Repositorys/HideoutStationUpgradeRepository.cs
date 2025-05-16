using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseObjects.BaseObject;
using TarkovTrackerDAL.Interfaces;
using TarkovTrackerBLL.Service;

namespace TarkovTrackerDAL.test
{
    public class HideoutStationUpgradeRepository : IHideoutStationUpgradeRepository
    {
        private readonly string _connectionString;

        public HideoutStationUpgradeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<HideoutStationUpgrade> GetAll()
        {
            var upgrades = new List<HideoutStationUpgrade>();
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM hideout_station_upgrade";
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        upgrades.Add(new HideoutStationUpgrade
                        {
                            StationId = reader.GetInt32(0),
                            ItemId = reader.GetInt32(1),
                            Amount = reader.GetInt32(2),
                            RequiredLevel = reader.GetInt32(3)
                        });
                    }
                }
            }
            return upgrades;
        }

        public HideoutStationUpgrade GetByStationIdAndItemId(int stationId, int itemId)
        {
            HideoutStationUpgrade upgrade = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM hideout_station_upgrade WHERE station_id = @stationId AND item_id = @itemId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@stationId", stationId);
                command.Parameters.AddWithValue("@itemId", itemId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        upgrade = new HideoutStationUpgrade
                        {
                            StationId = reader.GetInt32(0),
                            ItemId = reader.GetInt32(1),
                            Amount = reader.GetInt32(2),
                            RequiredLevel = reader.GetInt32(3)
                        };
                    }
                }
            }
            return upgrade;
        }

        public bool Add(HideoutStationUpgrade upgrade)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO hideout_station_upgrade (station_id, item_id, amount, required_level) VALUES (@stationId, @itemId, @amount, @requiredLevel)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@stationId", upgrade.StationId);
                command.Parameters.AddWithValue("@itemId", upgrade.ItemId);
                command.Parameters.AddWithValue("@amount", upgrade.Amount);
                command.Parameters.AddWithValue("@requiredLevel", upgrade.RequiredLevel);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int stationId, int itemId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM hideout_station_upgrade WHERE station_id = @stationId AND item_id = @itemId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@stationId", stationId);
                command.Parameters.AddWithValue("@itemId", itemId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(HideoutStationUpgrade upgrade)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE hideout_station_upgrade SET amount = @amount, required_level = @requiredLevel WHERE station_id = @stationId AND item_id = @itemId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@amount", upgrade.Amount);
                command.Parameters.AddWithValue("@requiredLevel", upgrade.RequiredLevel);
                command.Parameters.AddWithValue("@stationId", upgrade.StationId);
                command.Parameters.AddWithValue("@itemId", upgrade.ItemId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
