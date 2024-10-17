using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FiftyByte_POC.Models.leave_management
{
    public class LeaveApplication
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        // Foreign Key to LeavePolicy
        public int PolicyId { get; set; }

        [ForeignKey(nameof(PolicyId))]
        [JsonIgnore]
        public LeavePolicy? LeavePolicy { get; set; }

        // Foreign Key to CostCenter
        public int CostCenterId { get; set; }

        [ForeignKey(nameof(CostCenterId))]
        [JsonIgnore]
        public CostCenter? CostCenter { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string Reason { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";
    }
}
