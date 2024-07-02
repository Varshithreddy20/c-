namespace CropDev.Models.FPT
{
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

    public class FarmerPaymentTransaction : CreateFarmerPaymentTransaction
    {
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}