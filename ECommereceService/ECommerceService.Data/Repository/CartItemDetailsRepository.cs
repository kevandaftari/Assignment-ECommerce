using ECommerceService.Data.IRepository;
using ECommerceService.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ECommerceService.Entity.EFCoreInMemoryDb;

namespace ECommerceService.Data.Repository
{
    public class CartItemDetailsRepository : ICartItemDetailsRepository
    {
        public CartItemDetailsRepository()
        {
        }

        public ICollection<CartItemDetails> GetCartItemDetailsByUserId(int userId)
        {
            using (var context = new ApiContext())
            {
                var cartItemDetails = context.CartItemDetails.Where(item => item.CartUserId == userId).ToList();
                return cartItemDetails;

            }
        }

        public bool EmptyCart(int userId)
        {
            using (var context = new ApiContext())
            {
                var cartItems = context.CartItemDetails.Where(item => item.CartUserId == userId).ToList();
                if (cartItems.Count > 0)
                {
                    context.CartItemDetails.RemoveRange(cartItems);
                    return context.SaveChanges() > 0 ? true : false;
                }
                return true;
            }
        }
    }
}
