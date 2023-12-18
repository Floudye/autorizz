using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WPF_LoginForm.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            _connectionString = "Server=USER\\BATOSHA;Database=mvvm; Integrated Security=true";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
        public bool logcheck(string Username)
        {
            bool exists;

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM [users] WHERE Username=@Username";
                command.Parameters.AddWithValue("@Username", Username);

                exists = (int)command.ExecuteScalar() > 0;
            }

            return exists;
        }

    }
}
