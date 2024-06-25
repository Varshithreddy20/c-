using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Services;

namespace StudentApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/student
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _studentService.GetAllStudents();
            return Ok(students);
        }

        // GET: api/student/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // POST: api/student
        [HttpPost]
        public IActionResult Post(Student student)
        {
            _studentService.AddStudent(student);
            return CreatedAtAction(nameof(Get), new { id = student.StudentID }, student);
        }

        // PUT: api/student/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, Student student)
        {
            if (id != student.StudentID)
            {
                return BadRequest();
            }

            _studentService.UpdateStudent(student);
            return NoContent();
        }

        // DELETE: api/student/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _studentService.DeleteStudent(id);
            return NoContent();
        }
    }
}
