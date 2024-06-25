namespace StudentApp.Models
{
    public class Student
    {
        public Guid StudentID { get; set; } // Corresponds to StudentID column in the table
        public string FirstName { get; set; } // Corresponds to FirstName column in the table
        public string LastName { get; set; } // Corresponds to LastName column in the table
        public DateTime DateOfBirth { get; set; } // Corresponds to DateOfBirth column in the table
        public string Gender { get; set; } // Corresponds to Gender column in the table
        public string Email { get; set; } // Corresponds to Email column in the table
        public string Class { get; set; } // Corresponds to Class column in the table
        public int Age { get; set; } // Corresponds to Age column in the table

        //public bool IsDeleted { get; set; }// new column for soft delete in the table
    }
}

