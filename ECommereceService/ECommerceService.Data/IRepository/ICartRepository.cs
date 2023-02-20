
using ECommerceService.Entity.DTO;
using ECommerceService.Entity.Models;

namespace ECommerceService.Data.IRepository
{
    public interface ICartRepository
    {
        public Cart? AddItem(AddCartItemDTO cartItemToAdd);
    }
}
