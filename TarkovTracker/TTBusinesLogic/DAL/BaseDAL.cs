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

        private string connectionString;
        public BaseDAL(string ConnectionString)
        {
            connectionString = ConnectionString;
        }
        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
        protected SqlCommand QueryBuilder(SqlConnection conn, string query, string[] parameters, object[] values)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            for (int i = 0; i < parameters.Length; i++)
            {
                if (values.Length == parameters.Length)
                {
                    cmd.Parameters.AddWithValue(parameters[i], values[i] ?? (object)DBNull.Value);
                }
            }
            return cmd;
        }
        protected SqlCommand QueryBuilder(SqlConnection conn, string query)
        {
            return QueryBuilder(conn, query, [], []);
        }
    }
}
