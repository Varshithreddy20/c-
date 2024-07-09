using CropDev.Models;
using CropDev.Repository.Interface;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CropDev.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int SignUp(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("dbo.UserSignup", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@EmailId", user.EmailId);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Role", user.Role);

                SqlParameter statusOutput = new SqlParameter("@StatusOutput", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(statusOutput);

                connection.Open();
                command.ExecuteNonQuery();

                return (int)statusOutput.Value;
            }
        }

        public (bool, string) Login(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("dbo.UserLogin", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@EmailId", email);
                command.Parameters.AddWithValue("@Password", password);

                SqlParameter tokenOutput = new SqlParameter("@Token", SqlDbType.NVarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(tokenOutput);

                SqlParameter statusOutput = new SqlParameter("@StatusOutput", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(statusOutput);

                connection.Open();
                command.ExecuteNonQuery();

                bool success = (int)statusOutput.Value == 1;
                string token = tokenOutput.Value.ToString();

                return (success, token);
            }
        }
    }
}
