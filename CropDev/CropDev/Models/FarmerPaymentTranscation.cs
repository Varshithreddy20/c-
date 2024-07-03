namespace CropDev.Models.FPT
{
    /// <summary>
    /// Model for Creating Farmer Payment
    /// </summary>
    public class CreateFarmerPaymentTransaction
    {
        public int? FarmerPaymentTransactionId { get; set; }
        public int? FarmerLandDetailsId { get; set; }
        public int? PriceQuoteId { get; set; }
        public decimal? ChargeAmount { get; set; }
        public string? Discounts { get; set; }
        public decimal Paid { get; set; }
        public decimal Balance { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    /// <summary>
    /// Model for updating Framer Payment
    /// </summary>
    public class UpdateFarmerPaymentTransaction
    {   
        public int? FarmerPaymentTransactionId { get; set; }
        public int? FarmerLandDetailsId { get; set; }
        public int? PriceQuoteId { get; set; }
        public decimal? ChargeAmount { get; set; }
        public string? Discounts { get; set; }
        public decimal Paid { get; set; }
        public decimal Balance { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
    /// <summary>
    /// model for retrieving Farmer Payment 
    /// </summary>
    public class FarmerPaymentTransaction : CreateFarmerPaymentTransaction
    {
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}