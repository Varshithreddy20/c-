using System;

namespace CropDev.Models.FarmerRequest
{   
    /// <summary>
    /// Model for Creating Farmer Request
    /// </summary>
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
    /// <summary>
    /// Model for Updating Farmer Request
    /// </summary>
    public class UpdateFarmerRequest
    {
        public int? FarmerRequestId { get; set; }
        public int? FarmerLandDetailsId { get; set; }
        public string? RequestQuery { get; set; }
        public int? AgentUserId { get; set; }
        public byte? StatusId { get; set; }
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; }

    }
    /// <summary>
    /// Model for retrieving Farmer Request
    /// </summary>
    public class FarmerRequest: CreateFarmerRequest
    {
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; }

    }

}
