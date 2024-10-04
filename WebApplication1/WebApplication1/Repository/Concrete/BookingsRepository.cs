using CRAVENEST.Model.Bookings;
using CRAVENEST.Repository.Interface;
using CRAVENEST.Utilities;
using CRAVENEST.Utilities.Eums;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace CRAVENEST.Repository.Concrete
{
    public class BookingsRepository(IOptions<AppSettings> appSettings, ILogger<BookingsRepository> logger, IConfiguration configuration) : IBookingsRepository
    {
        private readonly AppSettings _appSettings = appSettings.Value;

        public async Task<ResultStatus> SoftDelete(int bookingId)
        {
            return await ExecuteNonQuery("[dbo].[SoftDeleteBookingById]", bookingId);
        }

        public async Task<ResultStatus> Restore(int bookingId)
        {
            return await ExecuteNonQuery("[dbo].[RestoreBookingById]", bookingId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        private async Task<ResultStatus> ExecuteNonQuery(string storedProcedure, int bookingId)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand(storedProcedure, sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@BookingId", SqlDbType.Int) { Value = bookingId });
                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error executing {storedProcedure} for BookingId {bookingId}");
                return ResultStatus.Failed;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingsId"></param>
        /// <returns></returns>
        public async Task<Bookings?> GetById(int bookingsId)
        {
            Bookings? booking = null;

            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[GetBookingById]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@BookingId", SqlDbType.Int) { Value = bookingsId });

                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    booking = new Bookings
                    {
                        BookingId = reader["BookingId"] != DBNull.Value ? Convert.ToInt32(reader["BookingId"]) : 0,
                        Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty,
                        Email = reader["Email"] != DBNull.Value ? Convert.ToString(reader["Email"]) : string.Empty,
                        Phone = reader["Phone"] != DBNull.Value ? Convert.ToString(reader["Phone"]) : string.Empty,
                        Persons = reader["Persons"] != DBNull.Value ? Convert.ToInt32(reader["Persons"]) : 0,
                        Status = reader["Status"] != DBNull.Value ? Convert.ToString(reader["Status"]) : "pending",
                        // Return null if no value in DB, not DateTime.MinValue
                        //BookingDateAndTime = reader["BookingDateAndTime"] != DBNull.Value ? (DateTime?)reader["BookingDateAndTime"] : null,
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error getting booking by id {bookingsId}");
            }

            return booking;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookings"></param>
        /// <returns></returns>
        public async Task<ResultStatus> Update(Bookings bookings)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[UpdateBookings]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@BookingId", SqlDbType.Int) { Value = bookings.BookingId });
                sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = bookings.Name });
                sqlCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = bookings.Email });
                sqlCommand.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 15) { Value = bookings.Phone });
                sqlCommand.Parameters.Add(new SqlParameter("@Persons", SqlDbType.Int) { Value = bookings.Persons });
                sqlCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 20) { Value = bookings.Status ?? "pending" });
                // sqlCommand.Parameters.Add(new SqlParameter("@BookingDateAndTime", SqlDbType.DateTime)
                //{
                //    Value = (object)bookings.BookingDateAndTime ?? DBNull.Value
                //});

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error updating booking with id {bookings.BookingId}");
                return ResultStatus.Failed;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookings"></param>
        /// <returns></returns>
        public async Task<ResultStatus> Create(Bookings bookings)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[CreateBookings]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = bookings.Name ?? (object)DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = bookings.Email ?? (object)DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 15) { Value = bookings.Phone ?? (object)DBNull.Value });
                sqlCommand.Parameters.Add(new SqlParameter("@Persons", SqlDbType.Int) { Value = bookings.Persons });
                sqlCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 20) { Value = bookings.Status ?? "pending" });
                sqlCommand.Parameters.Add(new SqlParameter("@SignUpId", SqlDbType.Int) { Value = bookings.SignUpId }); // Ensure SignUpId is sent

                // Add the output parameter for StatusOutput
                var statusOutputParam = new SqlParameter("@StatusOutput", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                sqlCommand.Parameters.Add(statusOutputParam);

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                // Retrieve the value of the output parameter after execution
                int statusOutput = (int)statusOutputParam.Value;

                // Check the value of statusOutput and return appropriate status
                if (statusOutput == 1)
                {
                    return ResultStatus.Success;
                }
                else if (statusOutput == -2)
                {
                    return ResultStatus.DuplicateEntry;  // Add this result status if required
                }
                else
                {
                    return ResultStatus.Failed;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating booking");
                return ResultStatus.Failed;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Bookings>> GetAll()
        {
            var bookingsList = new List<Bookings>();

            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[GetAllBookings]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var booking = new Bookings
                    {
                        BookingId = reader["BookingId"] != DBNull.Value ? Convert.ToInt32(reader["BookingId"]) : 0,
                        Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty,
                        Email = reader["Email"] != DBNull.Value ? Convert.ToString(reader["Email"]) : string.Empty,
                        Phone = reader["Phone"] != DBNull.Value ? Convert.ToString(reader["Phone"]) : string.Empty,
                       Persons = reader["Persons"] != DBNull.Value ? Convert.ToInt32(reader["Persons"]) : 0,
                        Status = reader["Status"] != DBNull.Value ? Convert.ToString(reader["Status"]) : "pending",
                        // Return null if no value in DB, not DateTime.MinValue
                        //BookingDateAndTime = reader["BookingDateAndTime"] != DBNull.Value ? (DateTime?)reader["BookingDateAndTime"] : null,
                    };

                    bookingsList.Add(booking);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all bookings");
            }

            return bookingsList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<ResultStatus> UpdateStatus(int bookingId, string status)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[UpdateBookingStatus]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@BookingId", SqlDbType.Int) { Value = bookingId });
                sqlCommand.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 20) { Value = status });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error updating booking status for BookingId {bookingId}");
                return ResultStatus.Failed;
            }
        }

        public async Task<List<Bookings>> GetBookingsBySignUpId(int signUpId)
        {
            var bookingsList = new List<Bookings>();

            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[GetBookingsBySignUpId]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@SignUpId", SqlDbType.Int) { Value = signUpId });

                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var booking = new Bookings
                    {
                        BookingId = reader["BookingId"] != DBNull.Value ? Convert.ToInt32(reader["BookingId"]) : 0,
                        Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty,
                        Email = reader["Email"] != DBNull.Value ? Convert.ToString(reader["Email"]) : string.Empty,
                        Phone = reader["Phone"] != DBNull.Value ? Convert.ToString(reader["Phone"]) : string.Empty,
                        Persons = reader["Persons"] != DBNull.Value ? Convert.ToInt32(reader["Persons"]) : 0,
                        Status = reader["Status"] != DBNull.Value ? Convert.ToString(reader["Status"]) : "pending",
                    };
                    bookingsList.Add(booking);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error retrieving bookings for SignUpId {signUpId}");
            }

            return bookingsList;
        }

    }
}

