
using ECommerceService.Entity.DTO;
using ECommerceService.Entity.Models.OrderDetails;

namespace ECommerceService.Data.IRepository
{
    public interface IOrderRepository
    {
        public Tuple<int, OrderDetails?> PlaceOrder(PlaceOrderDTO placeOrder);  
    }
}
