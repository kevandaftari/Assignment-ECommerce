using ECommerceService.Data.IRepository;
using ECommerceService.Entity.DTO;
using ECommerceService.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ECommerceService.Entity.EFCoreInMemoryDb;

namespace ECommerceService.Data.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ICartItemDetailsRepository _cartItemDetailsRepository;
        public CartRepository(ICartItemDetailsRepository cartItemDetailsRepository) 
        { 
            _cartItemDetailsRepository= cartItemDetailsRepository;
        }

        public Cart? AddItem(AddCartItemDTO cartItemToAdd)
        {
            using (var context = new ApiContext())
            {
                var item = context.Items.Find(cartItemToAdd.ItemId);
                if (item == null || item.RemainingCount < cartItemToAdd.Count)
                {
                    return null;
                }
                var cart = context.Carts.FirstOrDefault(x => x.UserId == cartItemToAdd.UserId);
                if (cart == null)
                {
                    cart = new Cart()
                    {
                        UserId= cartItemToAdd.UserId,
                    };
                    context.Carts.Add(cart);
                }

                cart.Items = context.CartItemDetails.Where(cDetails => cDetails.CartUserId == cartItemToAdd.UserId).ToList();
                var cartItemDetailToUpdate = cart.Items.FirstOrDefault(x => x.ItemId == cartItemToAdd.ItemId);

                if (cartItemDetailToUpdate == null)
                {
                    cartItemDetailToUpdate = new CartItemDetails()
                    {
                        ItemId = cartItemToAdd.ItemId,
                        Count = cartItemToAdd.Count,
                    };
                    cart.Items.Add(cartItemDetailToUpdate);   
                }
                else if(item.RemainingCount < cartItemDetailToUpdate.Count + cartItemToAdd.Count)
                {
                    return null;
                }
                else
                {
                    cartItemDetailToUpdate.Count = cartItemDetailToUpdate.Count + cartItemToAdd.Count;
                }
                context.SaveChanges();
                return cart;
            }
        }
    }
}
