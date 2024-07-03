namespace CropDev.Models.Farmers
{
    /// <summary>
    /// Model for Creating Farmers
    /// </summary>
    public class CreateFarmers
    {
        public int? FarmerId { get; set; } 
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set;} = string.Empty;
        public string? City {  get; set; } = string.Empty;
        public string? State {  get; set; } = string.Empty;
        public int? ZipCode { get; set;}
        public string? Country { get; set; } = string.Empty;
        public string? PhoneNumber {  get; set; } = string.Empty;
        public string? SecondaryPhoneNumber {  get; set; } = string.Empty;
        public string? CreatedBy {  get; set; } = string.Empty;
        public DateTime? CreatedOn { get; set; } = DateTime.Now;

    }
    /// <summary>
    /// Model for Updating Farmers
    /// </summary>
    public class UpdateFarmers
    {
        public int? FarmerId { get; set; }
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
    /// Model for retrieving Farmers
    /// </summary>
    public class Farmers: CreateFarmers
    {
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;

    }
}
