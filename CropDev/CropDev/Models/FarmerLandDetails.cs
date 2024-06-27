namespace CropDev.Models
{
    public class FarmerLandDetails
    {
        public int? FarmerLandDetailsId { get; set; }
        public int? FarmerId { get; set; }
        public string LandLocation { get; set; } = string.Empty;
        public int? Zipcode { get; set; }
        public decimal? LandSize { get; set; }
        public bool IsElectricityAvailable { get; set; }
        public bool IsWaterAvailable { get; set; }
        public short? SoilTypeId { get; set; }
        public string LastCrop { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
        public string FarmerName { get; set; } = string.Empty; 
        public string SoilTypeName { get; set; } = string.Empty; 
    }
}
