using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace TTBusinesLogic.DAL
{
    public class BaseDAL
    {
        public string test = "";
        private string connectionstring = "";
        public BaseDAL(string ConnectionString)
        {
            connectionstring = ConnectionString;
        }
        
        public void Connection()
        {
            SqlConnection sqlconnection = new SqlConnection(connectionstring);
            sqlconnection.Open();
            string query = "select * from user_info";
            using (SqlCommand cmd = new SqlCommand(query, sqlconnection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        test += " " + reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString() + " " + reader[4].ToString() + "\n";
                        
                    }
                }
            }
        }
    }
}
