using System;

namespace CropDev.Models.FarmerRequest
{
    public class CreateFarmerRequest
    {
        public int? FarmerRequestId { get; set; }
        public int? FarmerLandDetailsId { get; set; }
        public string? RequestQuery { get; set; }
        public int? AgentUserId { get; set; }
        public byte? StatusId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
       
    }
    public class UpdateFarmerRequest
    {
        public int? FarmerRequestId { get; set; }
        public int? FarmerLandDetailsId { get; set; }
        public string? RequestQuery { get; set; }
        public int? AgentUserId { get; set; }
        public byte? StatusId { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; }

    }
    public class FarmerRequest: CreateFarmerRequest
    {
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; }

    }

}
