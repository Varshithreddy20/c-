
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiftyByte_POC.Models
{
    [Table("Country")]
        public class Country
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int CountryId { get; set; }

            [Required]
            [MaxLength(255)]
            public string CountryName { get; set; } =string.Empty;

            [Required]
            [MaxLength(10)]
            public string CountryCode { get; set; } = string.Empty; 
        }
}

