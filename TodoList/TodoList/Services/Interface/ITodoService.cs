using TodoList.Model;
using TodoList.Utilities.Enums;


namespace TodoList.Services.Interface

{
    public interface ITodoService
    {
        Task<List<Todo>> GetAll();
        Task<ResultStatus> Create(Todo todo);
        Task<ResultStatus> Update(Todo todo);
        Task<Todo> GetById(int Id);
        Task<ResultStatus> Delete(int Id);
    }
}
