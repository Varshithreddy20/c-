namespace StudentApp.Models
{
    public class Signup
    {
        public Guid UserID { get; set; } // Corresponds to StudentID column in the table
        public string firstname { get; set; } // Corresponds to FirstName column in the table
        public string lastname { get; set; } // Corresponds to LastName column in the table
        public string emailid { get; set; }
        public string password { get; set; }
    }
}
