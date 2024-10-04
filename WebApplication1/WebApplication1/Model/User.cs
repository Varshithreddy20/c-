namespace CRAVENEST.Model
{
    // Represents the core User model for sign-up and general user details
    public class User
    {
        public int? SignUpId { get; set; } // Nullable because it will be null for new users during sign-up
        public string? Name { get; set; } = string.Empty;
        public string? EmailId { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public int Role { get; set; } = 1; // Role is defaulted to 1 (for example, standard user)
    }

    // Model used for login requests
    public class Login
    {
        public string? EmailId { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public int? Role { get; set; } // Optional role for login, if needed
        public int? SignUpId { get; set; } // To track which user logs in
        public string? Name { get; set; } // Can return name along with login if necessary
    }

    // Model used for confirming password (like in the confirm-password endpoint)
    public class LoginModel
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
    }

    public class UpdateProfileModel
    {
        public int SignUpId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string? NewPassword { get; set; }
    }

}
