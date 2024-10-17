using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiftyByte_POC.Models
{
    [Table("Translations")]
    public class Translation
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string EnglishText { get; set; }

        [MaxLength(255)]
        public string ArabicText { get; set; }
    }
}
