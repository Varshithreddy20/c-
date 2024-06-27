namespace CropDev.Models
{
    public class PriceQuote
    {
        public int? PriceQuoteId { get; set; }
        public decimal? LandSize { get; set; }
        public decimal? QuoteAmount { get; set; }
        public string ? CreatedBy{  get; set; }= string.Empty;
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }=string.Empty;
        public DateTime UpdatedOn { get; set; }=DateTime.Now;
    }
}
