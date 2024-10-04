namespace CropDev.Models
{
    public class User
    {
        public int SignUpId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class Login
    {
        public string EmailId { get; set; }
        public string Password { get; set; }    
    }
}
