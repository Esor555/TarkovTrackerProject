using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TTBusinesLogic.BusinesLogic;
using TTBusinesLogic.Interfaces;

namespace TTBusinesLogic.DAL
{
    public  class UserRepository: BaseDAL, IuserRepository
    {
        private List<User> Users;
        public UserRepository(string ConnectionString) : base(ConnectionString) { }

        public bool Add(User user)
        {
            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();
                string query =
                    "INSERT INTO user_info (id, username, password_hash, level, role) VALUES(@Id, @Username, @Password_Hash, @UserLevel, @Role)";
                SqlCommand command = QueryBuilder(connection, query,
                    ["@Id", "@Username", "@Password_Hash", "@UserLevel", "@Role"],
                    [user.Id, user.Name, "passwordhash", user.Level, user.Role,]);
                int i = command.ExecuteNonQuery();
                if (i == 1)
                {
                    return false;
                }
                return true;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
