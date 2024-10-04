using CRAVENEST.Model;
using CRAVENEST.Repository.Interface;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CRAVENEST.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // SignUp: Registers new users
        public int SignUp(User user)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand("dbo.UserSignup", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@EmailId", user.EmailId);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Role", 1); // Always set Role as 1

            SqlParameter statusOutput = new SqlParameter("@StatusOutput", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(statusOutput);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return (int)statusOutput.Value;
        }

        // Login: Authenticates users
        public (bool, string, Login) Login(string email, string password)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
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

            SqlParameter roleOutput = new SqlParameter("@Role", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(roleOutput);

            SqlParameter signUpIdOutput = new SqlParameter("@SignUpId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(signUpIdOutput);

            SqlParameter nameOutput = new SqlParameter("@Name", SqlDbType.NVarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(nameOutput);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Login login = new Login
            {
                Role = (int)roleOutput.Value,
                SignUpId = (int)signUpIdOutput.Value,
                Name = nameOutput.Value.ToString(),
            };

            bool success = (int)statusOutput.Value == 1;
            string token = tokenOutput.Value != DBNull.Value ? tokenOutput.Value.ToString() : string.Empty;

            return (success, token, login);
        }

        // UpdateSignup: Updates user profile and password (optional)
        public int UpdateSignup(UpdateProfileModel user)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand("dbo.UpdateSignup", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@SignUpId", user.SignUpId);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@EmailId", user.EmailId);

            if (!string.IsNullOrEmpty(user.NewPassword))
            {
                command.Parameters.AddWithValue("@NewPassword", user.NewPassword);
            }
            else
            {
                command.Parameters.AddWithValue("@NewPassword", DBNull.Value);
            }

            SqlParameter statusOutput = new SqlParameter("@StatusOutput", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(statusOutput);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return (int)statusOutput.Value;
        }

        // ConfirmPassword: Confirms user's password before updates
        public bool ConfirmPassword(string email, string password)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand("dbo.ConfirmPassword", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@EmailId", email);
            command.Parameters.AddWithValue("@Password", password);

            SqlParameter statusOutput = new SqlParameter("@StatusOutput", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(statusOutput);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return (int)statusOutput.Value == 1; // Return true if the password matches
        }

    }
}
