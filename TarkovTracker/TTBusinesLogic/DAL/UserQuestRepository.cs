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
    public class UserQuestRepository : BaseDAL, IUserQuestRepository
    {
        public UserQuestRepository(string connectionString) : base(connectionString) { }
        public List<UserQuest> getall(int userId)
        {
            var userQuests = new List<UserQuest>();

            string query = @"
        SELECT q.id, q.title, q.description, q.required_level, q.previous_quest_id, q.trader_id, q.wiki_link,
               uq.status
        FROM user_quest uq
        JOIN quest q ON uq.quest_id = q.id
        WHERE uq.user_id = @UserId";

            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);

                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userQuests.Add(new UserQuest
                            {
                                UserId = userId,
                                QuestId = reader.GetInt32(0),
                                Status = reader.GetString(7),
                                Quest = new Quest
                                {
                                    Id = reader.GetInt32(0),
                                    Title = reader.GetString(1),
                                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    RequiredLevel = reader.GetInt32(3),
                                    PreviousQuestId = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                                    TraderId = reader.GetInt32(5),
                                    WikiLink = reader.IsDBNull(6) ? null : reader.GetString(6)
                                }
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in GetQuestsForUser: " + ex.Message);
                }
            }

            return userQuests;
        }

        public bool Add(UserQuest userQuest)
        {
            string query = "INSERT INTO user_quest (user_id, quest_id, status) VALUES (@userId, @questId, @status)";

            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userQuest.UserId);
                cmd.Parameters.AddWithValue("@questId", userQuest.QuestId);
                cmd.Parameters.AddWithValue("@status", userQuest.Status);

                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Add: " + ex.Message);
                    return false;
                }
            }
        }
        public bool Remove(int userId, int questId)
        {
            string query = "DELETE FROM user_quest WHERE user_id = @userId AND quest_id = @questId";

            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@questId", questId);

                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Remove: " + ex.Message);
                    return false;
                }
            }
        }

        public bool Update(UserQuest userQuest)
        {
            string query = "UPDATE user_quest SET status = @status WHERE user_id = @userId AND quest_id = @questId";

            using (SqlConnection conn = CreateConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@status", userQuest.Status);
                cmd.Parameters.AddWithValue("@userId", userQuest.UserId);
                cmd.Parameters.AddWithValue("@questId", userQuest.QuestId);

                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Update: " + ex.Message);
                    return false;
                }
            }
        }

      
    }

}
