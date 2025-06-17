using Microsoft.Data.SqlClient;
using BaseObjects.BaseObject;
using TarkovTrackerDAL.Interfaces;

namespace TarkovTrackerDAL.test
{
    public class UserHideoutRepository : IUserHideoutRepository
    {
        private readonly string _connectionString;

        public UserHideoutRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<UserHideout> GetAll(int userId)
        {
            var userHideouts = new List<UserHideout>();
            string query = @"
                SELECT uh.user_id, uh.station_id, uh.station_level, hs.name as station_name
                FROM user_hideout uh
                JOIN hideout_station hs ON uh.station_id = hs.id
                WHERE uh.user_id = @UserId";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userHideouts.Add(new UserHideout(
                            userId: reader.GetInt32(0),
                            stationId: reader.GetInt32(1),
                            stationLevel: reader.GetInt32(2)
                        ));
                    }
                }
            }

            return userHideouts;
        }

        public UserHideout GetByStationId(int userId, int stationId)
        {
            UserHideout userHideout = null;
            string query = @"
                SELECT user_id, station_id, station_level
                FROM user_hideout
                WHERE user_id = @UserId AND station_id = @StationId";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@StationId", stationId);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userHideout = new UserHideout(
                            userId: reader.GetInt32(0),
                            stationId: reader.GetInt32(1),
                            stationLevel: reader.GetInt32(2)
                        );
                    }
                }
            }

            return userHideout;
        }

        public bool Add(UserHideout userHideout)
        {
            string query = @"
                INSERT INTO user_hideout (user_id, station_id, station_level)
                VALUES (@UserId, @StationId, @StationLevel)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserId", userHideout.UserId);
                command.Parameters.AddWithValue("@StationId", userHideout.StationId);
                command.Parameters.AddWithValue("@StationLevel", userHideout.StationLevel);
                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool Remove(int userId, int stationId)
        {
            string query = @"
                DELETE FROM user_hideout
                WHERE user_id = @UserId AND station_id = @StationId";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@StationId", stationId);
                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(UserHideout userHideout)
        {
            string query = @"
                UPDATE user_hideout
                SET station_level = @StationLevel
                WHERE user_id = @UserId AND station_id = @StationId";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StationLevel", userHideout.StationLevel);
                command.Parameters.AddWithValue("@UserId", userHideout.UserId);
                command.Parameters.AddWithValue("@StationId", userHideout.StationId);
                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
} 