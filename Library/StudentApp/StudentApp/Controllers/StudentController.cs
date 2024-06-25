using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using StudentApp.Services;
using System;

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
            try
            {
                var students = _studentService.GetAllStudents();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/student/{id}
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var student = _studentService.GetStudentById(id);
                if (student == null)
                {
                    return NotFound();
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/student
        [HttpPost]
        public IActionResult Post(Student student)
        {
            try
            {
                _studentService.AddStudent(student);
                return Get(student.StudentID);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/student/{id}
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Student student)
        {
            try
            {
                if (id != student.StudentID)
                {
                    return BadRequest();
                }

                _studentService.UpdateStudent(student);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      
        // DELETE: api/student/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _studentService.SoftDeleteStudent(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/student/restore/{id}
        [HttpPut("restore/{id}")]
        public IActionResult Restore(Guid id)
        {
            try
            {
                _studentService.RestoreStudent(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
