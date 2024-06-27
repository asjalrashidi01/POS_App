using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ProductItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Name { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public int Type { get; set; }
        public int Category { get; set; }
    }
}