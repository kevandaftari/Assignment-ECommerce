using ECommerceService.Data.IRepository;
using ECommerceService.Entity.DTO;
using ECommerceService.Entity.Models;
using ECommerceService.Entity.Models.OrderDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using static ECommerceService.Entity.EFCoreInMemoryDb;

namespace ECommerceService.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ICartItemDetailsRepository _cartItemDetailsRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IOrderMetaDataRepository _orderMetaDataRepository;

        public OrderRepository(
            ICartItemDetailsRepository cartItemDetailsRepository,
            ICouponRepository couponRepository,
            IOrderMetaDataRepository orderMetaDataRepository
            )
        {
            _cartItemDetailsRepository = cartItemDetailsRepository;
            _couponRepository = couponRepository;
            _orderMetaDataRepository = orderMetaDataRepository;
        }

        public Tuple<int, OrderDetails?> PlaceOrder(PlaceOrderDTO placeOrder)
        {
            using (var context = new ApiContext())
            {
                var coupon = GetCouponIfAppliedandValid(placeOrder.CouponCode);
                if (coupon == null)
                {
                    return new Tuple<int, OrderDetails?>(0, null);
                }

                var cartItems = _cartItemDetailsRepository.GetCartItemDetailsByUserId(placeOrder.UserId).ToList();
                if (cartItems.Count == 0)
                {
                    return new Tuple<int, OrderDetails?>(1, null);
                }


                double grossAmount = 0, discountAmount = 0;
                var orderItemDetails = new OrderItemsDetails()
                {
                    NumberOfItemsInOrder = 0,
                    OrderItems = new List<OrderItem>()
                };

                orderItemDetails.OrderItems = GetAllItems(cartItems);
                foreach (var item in orderItemDetails.OrderItems)
                {
                    orderItemDetails.NumberOfItemsInOrder += item.ItemCount;
                    grossAmount += item.ItemPrice * item.ItemCount;
                }

                discountAmount = (grossAmount * coupon.DiscountPercentage) / 100;
                var orderDetails = new OrderDetails()
                {
                    OrderId = Guid.NewGuid(),
                    UserId = placeOrder.UserId,
                    CouponCodeName = coupon.Code,
                    CouponDisplayName = coupon.DisplayName,
                    IsDiscountApplied = !string.IsNullOrEmpty(coupon.Code),
                    DiscountPercentage = coupon.DiscountPercentage,
                    GrossAmount = grossAmount,
                    DiscountAmount = discountAmount,
                    FinalPurchaseAmount = grossAmount - discountAmount,
                    OrderItemDetails = orderItemDetails
                };
                AddOrder(orderDetails);
                return new Tuple<int, OrderDetails?>(2, orderDetails);
            }
        }

        private void AddOrder(OrderDetails orderDetails)
        {
            using (var context = new ApiContext())
            {
                context.ListOfOrderDetails.Add(orderDetails);
                context.SaveChanges();
                EmptyCart(orderDetails.UserId);
                UpdateOrderMetaData(
                    orderDetails.OrderItemDetails.NumberOfItemsInOrder,
                    orderDetails.DiscountAmount,
                    orderDetails.FinalPurchaseAmount
                    );
            }
        }

        private Coupon GetCouponIfAppliedandValid(string couponCodeName)
        {
            Coupon coupon = new Coupon()
            {
                Code = string.Empty,
                DiscountPercentage = 0,
                DisplayName = string.Empty,
                OrderStart = 0,
                OrderFrequency = 0
            };
            if (string.IsNullOrEmpty(couponCodeName)) { return coupon; }
            return _couponRepository.ValidateCoupon(couponCodeName);
        }

        private void EmptyCart(int userId)
        {
            _cartItemDetailsRepository.EmptyCart(userId);
        }

        private void UpdateOrderMetaData(long numberOfItemsOrdered, double totalDiscountAmount, double totalPurchaseAmount)
        {
            var orderData = _orderMetaDataRepository.GetOrderMetaData();
            if (orderData != null)
            {
                orderData.NumberOfOrders += 1;
                orderData.NumberOfItemsOrdered += numberOfItemsOrdered;
                orderData.TotalDiscountAmount += totalDiscountAmount;
                orderData.TotalPurchaseAmount += totalPurchaseAmount;

                _orderMetaDataRepository.UpdateOrderMetaData(orderData);
            }
        }

        private List<OrderItem> GetAllItems(List<CartItemDetails> ListcartItemDetails)
        {
            using (var context = new ApiContext())
            {
                var orderItems = new List<OrderItem>();
                var items = context.Items.ToList();

                foreach (var cartItem in ListcartItemDetails)
                {
                    var item = items.FirstOrDefault(i => i.Id == cartItem.ItemId);
                    if (item != null)
                    {
                        var orderItem = new OrderItem()
                        {
                            ItemId = cartItem.ItemId,
                            ItemCount = Math.Min(cartItem.Count, item.RemainingCount),
                            ItemPrice = item.Price,
                            ItemDescription = item.ItemDescription,
                            ItemName = item.Name
                        };
                        item.RemainingCount -= orderItem.ItemCount;
                        orderItems.Add(orderItem);
                    }
                }
                context.SaveChanges();
                return orderItems;
            }
        }
    }
}
