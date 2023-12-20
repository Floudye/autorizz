using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(string Username, string Password, string Name, string accesslvl)
        {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "insert into [users] (Username , Password, Name, AccessLvl) " +
                                          "values (@Username, @Password, @Name, @AccessLvl)";
                    command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = Username;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
                    command.Parameters.Add("@AccessLvl", SqlDbType.NVarChar).Value = accesslvl;
                    command.ExecuteNonQuery();
                }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [users] where username=@username and [password]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }
        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }
        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [users] where username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            AccessLvl = reader[4].ToString(),
                        };
                    }
                }
            }
            return user;
        }
        public void DeleteUsername(int userId)
        {
            SqlConnection connection = new SqlConnection("Data Source=USER\\BATOSHA;Initial Catalog=mvvm;Integrated Security=True");
            string cmd = "DELETE FROM [users] WHERE Id = @Id";
            SqlCommand deleteCommand = new SqlCommand(cmd, connection);
            deleteCommand.Parameters.AddWithValue("@Id", userId);

            try
            {
                connection.Open();
                deleteCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void EditUser(string newusername, string newpassword, string newname, string newaccesslvl)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [users] SET Password = @Password, Name=@Name, AccessLvl = @AccessLvl where Username = @Username";
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = newusername;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = newpassword;
                command.Parameters.Add("@Name", (object)newname ?? DBNull.Value);
                command.Parameters.Add("@AccessLvl", SqlDbType.NVarChar).Value = newaccesslvl;
                command.ExecuteNonQuery();
            }
        }
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
        public void CreateUser(string login, string password, string acceslvl)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into [users] (Username , Password, AccessLvl) " + "values (@Username, @Password, @AccessLvl)";
                command.Parameters.AddWithValue("@Username", login);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@AccessLvl", acceslvl);
                command.ExecuteNonQuery();

            }
        }
        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand("select * from [users]", connection))
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new UserModel
                        {
                            Id = Convert.ToInt32(reader["Id"]).ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Name = reader["Name"].ToString(),
                            AccessLvl = reader["AccessLvl"].ToString(),


                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }
    }
}
