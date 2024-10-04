using TodoList.Repository.Interface;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using TodoList.Model;
using TodoList.Services.Concrete;
using TodoList.Services.Interface;
using TodoList.Utilities;
using System.Buffers.Text;
using TodoList.Utilities.Enums;
using Microsoft.Extensions.Configuration;

namespace TodoList.Repository.Concrete
{
    public class TodoRepository(IOptions<AppSettings> appSettings, ILogger<TodoRepository> logger, IConfiguration configuration) : ITodoRepository
    {
        private readonly AppSettings _appSettings = appSettings.Value;

        public async Task<Todo?> GetById(int id)
        {
            Todo? todo = null;

            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[GetTodoById]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    todo = new Todo
                    {
                        Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                        Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty,
                        Description = reader["Description"] != DBNull.Value ? Convert.ToString(reader["Description"]) : string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error getting task by id {id}");
            }

            return todo;
        }

        public async Task<ResultStatus> Update(Todo todo)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[UpdateTodo]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = todo.Id });
                sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100) { Value = todo.Name });
                sqlCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100) { Value = todo.Description });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error updating task with id {todo.Id}");
                return ResultStatus.Failed;
            }
        }

        public async Task<ResultStatus> Create(Todo todo)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[CreateTodo]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = todo.Name });
                sqlCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 100) { Value = todo.Description });

                var outputParameter = sqlCommand.Parameters.Add(new SqlParameter("@StatusOutput", SqlDbType.Int) { Direction = ParameterDirection.Output });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                int statusOutput = (int)outputParameter.Value;
                return statusOutput == 1 ? ResultStatus.Success : ResultStatus.Failed;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating task");
                return ResultStatus.Failed;
            }
        }

        public async Task<List<Todo>> GetAll()
        {
            var todos = new List<Todo>();

            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[GetAllTodos]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await sqlConnection.OpenAsync();
                using var reader = await sqlCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var todo = new Todo
                    {
                        Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                        Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : string.Empty,
                        Description = reader["Description"] != DBNull.Value ? Convert.ToString(reader["Description"]) : string.Empty
                    };

                    todos.Add(todo);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting all todos");
            }

            return todos;
        }
        public async Task<ResultStatus> Delete(int id)
        {
            try
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                using var sqlConnection = new SqlConnection(connectionString);
                using var sqlCommand = new SqlCommand("[dbo].[DeleteTodo]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();

                return ResultStatus.Success;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error deleting task with id {id}");
                return ResultStatus.Failed;
            }
        }

    }
}
