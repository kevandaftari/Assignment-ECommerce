
using System.ComponentModel.DataAnnotations;

namespace ECommerceService.Entity.Models
{
    public class OrderMetaData
    {
        [Key]
        public string Id { get; set; }
        public double TotalPurchaseAmount { get; set; }
        public double TotalDiscountAmount { get; set; }
        public long NumberOfItemsOrdered { get; set; }
        public long NumberOfOrders { get; set; }
    }
}
