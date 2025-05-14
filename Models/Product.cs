using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ST10070933PROG7311.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; }

        public int FarmerId { get; set; } 

        [ForeignKey("FarmerId")]
        public Farmer? Farmer { get; set; } 
    }
}
