using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TodoList.Model;
using TodoList.Repository.Interface;
using TodoList.Utilities.Enums;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository todoRepository;
        private readonly HttpClient httpClient;

        public TodoController(ITodoRepository todoRepository, HttpClient httpClient)
        {
            this.todoRepository = todoRepository;
            this.httpClient = httpClient;
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            try
            {
                var todos = await todoRepository.GetAll();
                return Ok(todos);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/todo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(int id)
        {
            try
            {
                var todo = await todoRepository.GetById(id);

                if (todo == null)
                {
                    return NotFound($"Todo with ID {id} not found.");
                }

                return Ok(todo);
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult> CreateTodo([FromBody] Todo todo)
        {
            if (todo == null)
            {
                return BadRequest("Todo cannot be null.");
            }

            try
            {
                var result = await todoRepository.Create(todo);

                if (result == ResultStatus.Success)
                {
                    return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
                }

                return StatusCode(500, "A problem occurred while handling your request.");
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/todo/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodo(int id, [FromBody] Todo todo)
        {
            if (todo == null || todo.Id != id)
            {
                return BadRequest("Todo object is null or ID mismatch.");
            }

            try
            {
                var existingTodo = await todoRepository.GetById(id);

                if (existingTodo == null)
                {
                    return NotFound($"Todo with ID {id} not found.");
                }

                var result = await todoRepository.Update(todo);

                if (result == ResultStatus.Success)
                {
                    return Ok(todo);
                }

                return StatusCode(500, "A problem occurred while handling your request.");
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            try
            {
                var existingTodo = await todoRepository.GetById(id);

                if (existingTodo == null)
                {
                    return NotFound($"Todo with ID {id} not found.");
                }

                var result = await todoRepository.Delete(id);

                if (result == ResultStatus.Success)
                {
                    return NoContent();
                }

                return StatusCode(500, "A problem occurred while handling your request.");
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
