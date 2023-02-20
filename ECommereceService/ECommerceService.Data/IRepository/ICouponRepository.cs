using ECommerceService.Entity.Models;

namespace ECommerceService.Data.IRepository
{
    public interface ICouponRepository
    {
        public ICollection<Coupon> GetAllCoupons();
        public Coupon? AddCoupon(Coupon coupon);

        public Coupon ValidateCoupon(string couponCode);
    }
}
