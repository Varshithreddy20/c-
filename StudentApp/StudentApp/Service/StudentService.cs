using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using StudentApp.Models;

namespace StudentApp.Services
{
    public class StudentService : IStudentService
    {
        private readonly string _connectionString;

        public StudentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Student> GetAllStudents()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Students_data";
                return connection.Query<Student>(query);
            }
        }

        public Student GetStudentById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Students_data WHERE StudentID = @StudentID";
                return connection.QueryFirstOrDefault<Student>(query, new { StudentID = id });
            }
        }

        public void AddStudent(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Students_data (FirstName, LastName, DateOfBirth, Gender, Email, Class, Age) 
                                 VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @Class, @Age )";
                connection.Execute(query, student);
            }
        }

        public void UpdateStudent(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Students_data 
                                 SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, 
                                     Email = @Email, Class = @Class, Age = @Age
                                 WHERE StudentID = @StudentID";
                connection.Execute(query, student);
            }
        }

        public void DeleteStudent(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Students_data WHERE StudentID = @StudentID";
                connection.Execute(query, new { StudentID = id });
            }
        }
    }
}
