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
            if (parameters.Length != values.Length)
                throw new ArgumentException("Parameters and values count do not match");

            SqlCommand cmd = new SqlCommand(query, conn);
            for (int i = 0; i < parameters.Length; i++)
            {
                cmd.Parameters.AddWithValue(parameters[i], values[i] ?? DBNull.Value);
            }
            return cmd;
        }

        protected SqlCommand QueryBuilder(SqlConnection conn, string query)
        {
            return QueryBuilder(conn, query, [], []);
        }

    }
}
