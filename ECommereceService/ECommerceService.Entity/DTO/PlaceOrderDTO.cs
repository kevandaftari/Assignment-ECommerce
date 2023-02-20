using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Entity.DTO
{
    public class PlaceOrderDTO
    {
        public int UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
