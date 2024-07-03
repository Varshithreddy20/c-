namespace CropDev.Models.AgentUsers
{
    /// <summary>
    /// Model for creating an agentUser
    /// </summary>
    public class CreateAgentUser
    {
        public int? AgentUserId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public int? ZipCode { get; set; }
        public string? Country { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? SecondaryPhoneNumber { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
    }

    
    /// <summary>
    /// Model for updating an agent
    /// </summary>
    public class UpdateAgentUser
    {
        public int? AgentUserId { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public int? ZipCode { get; set; }
        public string? Country { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? SecondaryPhoneNumber { get; set; } = string.Empty;
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
    }

    
    /// <summary>
    ///  Model for retrieving an agent
    /// </summary>
    public class AgentUser : CreateAgentUser
    {
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
    }
}
