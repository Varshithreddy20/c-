using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PG.Model
{
    public class PGInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PG_ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string PG_Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PG_Type { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string PG_Location { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string PG_State { get; set; } = string.Empty;

        [Required]
        public long PG_ContactNumber { get; set; }

        [Required]
        [MaxLength(3)]
        public string PG_Rooms_Availability { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Food { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Washing_Machine { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Refrigerator { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Self_Cooking { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Water_Availability { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Cleaning { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Bike_Parking { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Car_Parking { get; set; } = string.Empty;
    }
}
