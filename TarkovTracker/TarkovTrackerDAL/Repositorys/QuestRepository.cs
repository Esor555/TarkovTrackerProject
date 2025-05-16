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

    public class QuestRepository : IquestRepository
    {
        private readonly string _connectionString;

        public QuestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Quest> GetAll()
        {
            List<Quest> quests = new List<Quest>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM quest";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        quests.Add(MapToQuest(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return quests;
        }

        public Quest GetByName(string title)
        {
            Quest quest = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM quest WHERE title = @title";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@title", title);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        quest = MapToQuest(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return quest;
        }

        public Quest GetById(int id)
        {
            Quest quest = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM quest WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        quest = MapToQuest(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return quest;
        }

        public bool Add(Quest quest)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    INSERT INTO quest (title, description, required_level, previous_quest_id, trader_id, wiki_link)
                    VALUES (@title, @description, @required_level, @previous_quest_id, @trader_id, @wiki_link)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@title", quest.Title);
                command.Parameters.AddWithValue("@description", quest.Description ?? "");
                command.Parameters.AddWithValue("@required_level", quest.RequiredLevel);
                command.Parameters.AddWithValue("@previous_quest_id", (object)quest.PreviousQuestId ?? DBNull.Value);
                command.Parameters.AddWithValue("@trader_id", quest.TraderId);
                command.Parameters.AddWithValue("@wiki_link", quest.WikiLink ?? "");

                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM quest WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public bool Update(Quest quest)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE quest
                    SET title = @title,
                        description = @description,
                        required_level = @required_level,
                        previous_quest_id = @previous_quest_id,
                        trader_id = @trader_id,
                        wiki_link = @wiki_link
                    WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@title", quest.Title);
                command.Parameters.AddWithValue("@description", quest.Description ?? "");
                command.Parameters.AddWithValue("@required_level", quest.RequiredLevel);
                command.Parameters.AddWithValue("@previous_quest_id", (object)quest.PreviousQuestId ?? DBNull.Value);
                command.Parameters.AddWithValue("@trader_id", quest.TraderId);
                command.Parameters.AddWithValue("@wiki_link", quest.WikiLink ?? "");
                command.Parameters.AddWithValue("@id", quest.Id);

                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        private Quest MapToQuest(SqlDataReader reader)
        {
            return new Quest()
            {
                Id = Convert.ToInt32(reader["id"]),
                Title = reader["title"].ToString(),
                Description = reader["description"]?.ToString(),
                RequiredLevel = Convert.ToInt32(reader["required_level"]),
                PreviousQuestId = reader["previous_quest_id"] == DBNull.Value ? null : Convert.ToInt32(reader["previous_quest_id"]),
                TraderId = Convert.ToInt32(reader["trader_id"]),
                WikiLink = reader["wiki_link"]?.ToString()
            };
        }
    }
}
