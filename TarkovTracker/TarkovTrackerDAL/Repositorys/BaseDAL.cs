using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace TarkovTrackerDAL.test
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
        }
}
