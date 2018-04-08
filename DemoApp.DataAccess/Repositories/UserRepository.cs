using DemoApp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.DataAccess.Repositories
{
    public class UserRepository : BaseRepository
    {
            
        public virtual UserRepository Init(string dataBaseName)
        {
            base.Init(dataBaseName);
            return this;
        }

        public virtual List<User> GetAll()
        {
            List<User> result = new List<User>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("UsersGetAll", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            Id = reader.GetGuid(0),
                            Email = reader.GetString(1),
                            Password = reader.GetString(2),
                        };
                        result.Add(user);
                    }

                }
            }
            return result;
        }

        public virtual User FindByEmail(string email)
        {
            List<User> result = new List<User>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("UsersFindByEmail", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Email", email);
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            Id = reader.GetGuid(0),
                            Email = reader.GetString(1),
                            Password = reader.GetString(2),
                        };
                        result.Add(user);
                    }
                }
            }
            return result.FirstOrDefault();
        }

        public User Validate(string email, string password)
        {
            List<User> result = new List<User>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("UsersValidate", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Email", email);
                command.Parameters.AddWithValue("Password", password);
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            Id = reader.GetGuid(0),
                            Email = reader.GetString(1),
                            Password = reader.GetString(2),
                        };
                        result.Add(user);
                    }
                }
            }
            return result.FirstOrDefault();
        }

        public virtual Guid Create(User user)
        {
            Guid result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                
                SqlCommand command = new SqlCommand("UsersInsert", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Email", user.Email);
                command.Parameters.AddWithValue("Password", user.Password);
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Direction = ParameterDirection.Output;
                conn.Open();
                command.ExecuteNonQuery();
                result = new Guid(command.Parameters["@Id"].Value.ToString());
            }
            return result;
        }

        public int Update(User user)
        {
            int result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                SqlCommand command = new SqlCommand("UsersUpdate", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Email", user.Email);
                command.Parameters.AddWithValue("Password", user.Password);
                conn.Open();
                result = command.ExecuteNonQuery();
            }
            return result;
        }

        public int Delete(Guid Id)
        {
            int result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                SqlCommand command = new SqlCommand("UsersDelete", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Id", Id);
                conn.Open();
                result = command.ExecuteNonQuery();
            }
            return result;
        }
    }
}
