using System.ComponentModel.DataAnnotations;

namespace FiftyByte_POC.Models.leave_management
{
    public class LeavePolicy
    {
        [Key]
        public int PolicyId { get; set; }

        [MaxLength(255)]
        public string PolicyName { get; set; }
    }
}
