using POS_App;

namespace WebAPI.Models
{
    public class SaleDTO
    {
        public List<ProductEntity> Products { get; set; }
        public float Total { get; set; }
    }
}
