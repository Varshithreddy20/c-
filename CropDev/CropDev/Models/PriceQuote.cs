using System;

namespace CropDev.Models.PriceQuote
{   
    /// <summary>
    /// Model for Creating Price Quote
    /// </summary>
    public class CreatePriceQuote
    {
        public int? PriceQuoteId { get; set; }
        public decimal? LandSize { get; set; }
        public decimal? QuoteAmount { get; set; }
        public string? CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
       
    }
    /// <summary>
    /// Model for Updating Price Quote
    /// </summary>
    public class UpdatePriceQuote
    {
        public int? PriceQuoteId { get; set; }
        public decimal? LandSize { get; set; }
        public decimal? QuoteAmount { get; set; }
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
    /// <summary>
    /// Model for retrieving Farmers
    /// </summary>
    public class PriceQuote:CreatePriceQuote
    {
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
