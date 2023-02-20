using ECommerceService.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Data.IRepository
{
    public interface ICartItemDetailsRepository
    {
        public ICollection<CartItemDetails> GetCartItemDetailsByUserId(int userId);
        public bool EmptyCart(int userId);

    }
}
