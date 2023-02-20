using ECommerceService.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Business.IServices
{
    public interface ICouponService
    {
        public ICollection<string> GetListOfAllDiscountCodes();
        public Coupon AddCoupon(Coupon coupon);
    }
}
