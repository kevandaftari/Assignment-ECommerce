
using System.ComponentModel.DataAnnotations;

namespace ECommerceService.Entity.Models.OrderDetails
{
    public class OrderDetails
    {
        public OrderDetails()
        {
            this.OrderItemDetails = new OrderItemsDetails();
        }
        [Key]
        public Guid OrderId { get; set; }
        public int UserId { get; set; }
        public bool IsDiscountApplied { get; set; }
        public string CouponCodeName { get; set; }
        public string CouponDisplayName { get; set; }
        public float DiscountPercentage { get; set; }
        public double DiscountAmount { get; set; }
        public OrderItemsDetails OrderItemDetails { get; set; }
        public double GrossAmount { get; set; }
        public double FinalPurchaseAmount { get; set; }
    }
}

