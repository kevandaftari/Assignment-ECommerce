using ECommerceService.Data.IRepository;
using ECommerceService.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceService.API.Controllers
{
    [Route("[Controller]")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ILogger<CouponController> _logger;

        public CouponController(
            ICouponRepository couponRepository,
            ILogger<CouponController> logger
            ) 
        {
            _couponRepository = couponRepository;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public IActionResult Post([FromBody] Coupon coupon)
        {
            try
            {
                if(coupon == null 
                    || string.IsNullOrEmpty(coupon.Code)
                    || string.IsNullOrEmpty(coupon.DisplayName)
                    || coupon.OrderFrequency == 0
                   )
                {
                    return BadRequest();
                }
                var addedCoupon = _couponRepository.AddCoupon(coupon);
                if (addedCoupon == null)
                {
                    return NoContent();
                }
                if(addedCoupon.Code == "Duplicate")
                {
                    return Conflict("Coupon Code already Exists.");
                }
                return Ok(addedCoupon);

            }
            catch (Exception e)
            {
                _logger.LogError("error occured in CouponController Post:{Message}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpGet("[action]")]
        public IActionResult GetListOfCoupon()
        {
            try
            {
                
                var couponCodes = _couponRepository.GetAllCoupons();
                if (couponCodes == null || !couponCodes.Any())
                    return NoContent();
                return Ok(couponCodes);
                
            }
            catch (Exception e)
            {
                _logger.LogError("error occured inCouponController GetListOfCouponCode:{Message}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
