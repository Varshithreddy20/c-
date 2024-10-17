using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FiftyByte_POC.Models
{
    [Table("Company_Logo")] 
    public class CompanyLogo
    {
        [Key]
        public int Company_Logo_Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? LogoName { get; set; }

        [Required]
        public byte[] LogoData { get; set; } 

        [Required]
        [MaxLength(100)]
        public string ContentType { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
