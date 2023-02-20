using ECommerceService.Business.IServices;
using ECommerceService.Data.IRepository;
using ECommerceService.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Business.Services
{
    public class CouponService 
    {
        private readonly ICouponRepository _couponRepository;

        public CouponService(ICouponRepository couponRepository) 
        { 
            _couponRepository = couponRepository;
        }

    }
}
