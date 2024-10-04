using TodoList.Utilities.Enums;
using TodoList.Model;

namespace TodoList.Repository.Interface
{
    public interface ITodoRepository 
    {
        Task<List<Todo>> GetAll();
        Task<ResultStatus> Create(Todo todo);
        Task<ResultStatus> Update(Todo todo);
        Task<Todo> GetById(int Id);
        Task<ResultStatus> Delete(int Id);
    }
           
}
