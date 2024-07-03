namespace CropDev.Models.FarmerLadDetails
{   /// <summary>
/// Model for Creating FarmerLand Details
/// </summary>
    public class CreateFarmerLandDetails
    {
        public int? FarmerLandDetailsId { get; set; }
        public int? FarmerId { get; set; }
        public string? LandLocation { get; set; } = string.Empty;
        public int? Zipcode { get; set; }
        public decimal? LandSize { get; set; }
        public bool? IsElectricityAvailable { get; set; }
        public bool? IsWaterAvailable { get; set; }
        public short? SoilTypeId { get; set; }
        public string? LastCrop { get; set; } = string.Empty;
        public string?  CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public string? FarmerName { get; set; } = string.Empty;
        public string? SoilTypeName { get; set; } = string.Empty;
    }
    /// <summary>
    /// Model for Updating FarmerLand Details 
    /// </summary>
    public class UpdateFarmerLandDetails
    {
        public int? FarmerLandDetailsId { get; set; }
        public int? FarmerId { get; set; }
        public string? LandLocation { get; set; } = string.Empty;
        public int? Zipcode { get; set; }
        public decimal? LandSize { get; set; }
        public bool? IsElectricityAvailable { get; set; }
        public bool? IsWaterAvailable { get; set; }
        public short? SoilTypeId { get; set; }
        public string? LastCrop { get; set; } = string.Empty;
        public string? FarmerName { get; set; } = string.Empty;
        public string? SoilTypeName { get; set; } = string.Empty;
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;

    }
    /// <summary>
    /// Model for retrieving Farmer Land Details
    /// </summary>
    public class FarmerLandDetails : CreateFarmerLandDetails
    {
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;

    }
}
