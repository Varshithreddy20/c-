namespace CRAVENEST.Model.Bookings
{
    public class Bookings
    {   
        /// <summary>
        /// 
        /// </summary>
        public int? BookingId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string? Email { get; set; }=string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string? Phone { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public int? Persons { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Status { get; set; } = "pending";
        // public DateTime? BookingDateAndTime {  get; set; }
        public int? SignUpId { get; set; }
    }
}
