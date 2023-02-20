
namespace ECommerceService.Entity.Models.OrderDetails
{
    public class OrderItemsDetails
    {
        public OrderItemsDetails()
        {
            this.OrderItems = new List<OrderItem>();
        }
        public int NumberOfItemsInOrder { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        #region Navigation property
        public Guid OrderIdFK { get; set; }
        public OrderDetails orderDetails { get; set; }
        #endregion
    }

    public class OrderItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public float ItemPrice { get; set; }
        public int ItemCount { get; set; }

        #region Navigation Property
        public Guid OrderIdFK { get; set; }
        public OrderItemsDetails orderItemsDetails { get; set; }
        #endregion
    }
}
