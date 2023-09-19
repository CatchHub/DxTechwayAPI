using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.Entities
{
    public class Currency
    {
        [Key]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public char Symbol { get; set; }
    }
}
