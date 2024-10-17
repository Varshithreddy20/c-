using System.ComponentModel.DataAnnotations;

namespace FiftyByte_POC.Models.leave_management
{
    public class CostCenter
    {
        [Key]
        public int? CostCenterId { get; set; }
        public string? CostCenterName { get; set; }
    }

}
