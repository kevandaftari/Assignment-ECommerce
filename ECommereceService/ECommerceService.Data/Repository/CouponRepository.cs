using ECommerceService.Data.IRepository;
using ECommerceService.Entity.Models;
using static ECommerceService.Entity.EFCoreInMemoryDb;

namespace ECommerceService.Data.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IOrderMetaDataRepository _orderMetaDataRepository;

        public CouponRepository(
            IOrderMetaDataRepository orderMetaDataRepository
            )
        {
            _orderMetaDataRepository = orderMetaDataRepository;
        }

        public Coupon? AddCoupon(Coupon coupon)
        {
            using (var context = new ApiContext())
            {
                var coupons = context.Coupons.ToList();
                if(coupons.Exists(c => c.Code.Equals(coupon.Code)))
                {
                    return new Coupon()
                    {
                        Code = "Duplicate"
                    };
                }
                var numberOfOrders = context.OrderMetaData?.FirstOrDefault(x => x.Id == "OrderMetaData")?.NumberOfOrders ?? 0;
                var couponToBeAdded = new Coupon
                {
                    Code = coupon.Code,
                    DisplayName = coupon.DisplayName,
                    DiscountPercentage = coupon.DiscountPercentage,
                    OrderFrequency = coupon.OrderFrequency,
                    OrderStart = numberOfOrders + 1
                };
                context.Coupons.Add(couponToBeAdded);
                if(context.SaveChanges() > 0 )
                {
                    return couponToBeAdded;
                }
                return null;
            }
        }

        public ICollection<Coupon> GetAllCoupons()
        {
            using (var context = new ApiContext())
            {
                 return context.Coupons.ToList();
            }
        }

        private Coupon? GetCouponDataByCouponCode(string couponCode)
        {
            using (var context = new ApiContext())
            {
                return context.Coupons.FirstOrDefault(c => c.Code.Equals(couponCode));
            }
        }

        public Coupon? ValidateCoupon(string couponCode)
        {
            if (string.IsNullOrWhiteSpace(couponCode)) { return null; }
            var couponData = GetCouponDataByCouponCode(couponCode);
            if (couponData == null) { return null; }

            var metaData = _orderMetaDataRepository.GetOrderMetaData();
            if(metaData != null 
                &&
              (metaData.NumberOfOrders - couponData.OrderStart + 1) % couponData.OrderFrequency == 0)
            {
                return couponData;
            }
            return null;
        }
    }
}
