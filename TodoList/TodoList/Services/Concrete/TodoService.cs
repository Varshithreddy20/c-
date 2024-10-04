using TodoList.Services.Interface;
using TodoList.Utilities.Enums;
using TodoList.Model;
using TodoList.Repository.Interface;

namespace TodoList.Services.Concrete

{
    public class TodoService(ITodoRepository todoRepository) : ITodoService
    {
        public async Task<List<Todo>> GetAll()
        {
            return await todoRepository.GetAll();
        }

        public async Task<ResultStatus> Create(Todo todo)
        {
            return await todoRepository.Create(todo);
        }

        public async Task<ResultStatus> Update(Todo todo)
        {
            return await todoRepository.Update(todo);
        }

        public async Task<Todo> GetById(int Id)
        {
            return await todoRepository.GetById(Id);
        }
        public async Task<ResultStatus> Delete(int Id)
        {
            return await todoRepository.Delete(Id);
        }
    }
}
