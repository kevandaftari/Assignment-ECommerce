
using System.ComponentModel.DataAnnotations;

namespace ECommerceService.Entity.Models
{
    public class Coupon
    {
        [Key]
        public string Code { get; set; }
        public string DisplayName { get; set;}
        public float DiscountPercentage { get; set; }
        public int OrderFrequency { get; set; }
        public long OrderStart { get; set; }
    }
}
