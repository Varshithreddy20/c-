using System;
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
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Students_data WHERE is_deleted = 0 OR is_deleted IS NULL";
                    return connection.Query<Student>(query);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all students", ex);
            }
        }

        public Student GetStudentById(Guid id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT StudentID, FirstName, LastName, DateOfBirth, Gender, Email, Class, Age FROM Students_data WHERE StudentID = @StudentID AND (is_deleted = 0 OR is_deleted IS NULL)";
                    return connection.QueryFirstOrDefault<Student>(query, new { StudentID = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching the student with ID {id}", ex);
            }
        }

        public void AddStudent(Student student)
        {
            try
            {
                // Generate unique ID using GUID
                student.StudentID = Guid.NewGuid();

                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO Students_data (StudentID, FirstName, LastName, DateOfBirth, Gender, Email, Class, Age) 
                                     VALUES (@StudentID, @FirstName, @LastName, @DateOfBirth, @Gender, @Email, @Class, @Age)";
                    connection.Execute(query, student);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the student", ex);
            }
        }

        public void UpdateStudent(Student student)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = @"UPDATE Students_data 
                                     SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, 
                                         Email = @Email, Class = @Class, Age = @Age
                                     WHERE StudentID = @StudentID AND (is_deleted = 0 OR is_deleted IS NULL)";
                    connection.Execute(query, student);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the student with ID {student.StudentID}", ex);
            }
        }

        public void DeleteStudent(Guid id)
        {
            try
            {
                SoftDeleteStudent(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the student with ID {id}", ex);
            }
        }

        public void SoftDeleteStudent(Guid id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Students_data SET is_deleted = 1 WHERE StudentID = @StudentID";
                    connection.Execute(query, new { StudentID = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while soft-deleting the student with ID {id}", ex);
            }
        }

        public void RestoreStudent(Guid id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Students_data SET is_deleted = 0 WHERE StudentID = @StudentID";
                    connection.Execute(query, new { StudentID = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while restoring the student with ID {id}", ex);
            }
        }
    }
}
