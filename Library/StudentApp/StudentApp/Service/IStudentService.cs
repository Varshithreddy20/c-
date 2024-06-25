using System.Collections.Generic;
using StudentApp.Models;

namespace StudentApp.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(Guid id);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Guid id);
        void SoftDeleteStudent(Guid id);  
        void RestoreStudent(Guid id);     
    }
}
