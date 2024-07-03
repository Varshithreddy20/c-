using CropDev.Models.Signup;
using CropDev.Repository.Interface.SignUp;
using CropDev.Utilities;
using CropDev.Utilities.Enums;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;
using CropDev.Models.Login;

namespace CropDev.Repository.Concrete.SignUp
{
    public class SignUpRepository(IOptions<AppSettings> appSettings, ILogger<SignUpRepository> logger) : ISignUpRepository
    {
        public async Task<ResultStatus> Create(CreateSignUp createSignUp)
        {
            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[CreateSignup]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 100) { Value = createSignUp.FirstName });
                sqlCommand.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = createSignUp.LastName });
                sqlCommand.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar, 100) { Value = createSignUp.EmailId });
                sqlCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 100) { Value = createSignUp.Password });
                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutPut", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();
                int statusOutput = (int)outputParameter.Value;
                logger.LogInformation("CreateSignUp executed with status: {StatusOutput}", statusOutput);

                return statusOutput switch
                {
                    1 => ResultStatus.Success,
                    -2 => ResultStatus.DuplicateEntry,
                    _ => ResultStatus.Failed,
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while signing up");
                return ResultStatus.InternalServerError;
            }
        }

        public async Task<ResultStatus> ValidateLogin(string emailId, string password)
        {
            try
            {
                using var sqlConnection = new SqlConnection(appSettings.Value.FarmersDBConnection);
                using var sqlCommand = new SqlCommand("[dbo].[CreateLogin]", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar, 100) { Value = emailId });
                sqlCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 100) { Value = password });
                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();
                int statusOutput = (int)outputParameter.Value;
                logger.LogInformation("ValidateLogin executed with status: {StatusOutput}", statusOutput);

                return statusOutput switch
                {
                    1 => ResultStatus.Success,
                    -1 => ResultStatus.Failed,
                    _ => ResultStatus.Failed,
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while validating login");
                return ResultStatus.InternalServerError;
            }
        }

    }
}