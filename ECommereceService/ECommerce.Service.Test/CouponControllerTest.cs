using ECommerceService.API.Controllers;
using ECommerceService.Data.IRepository;
using ECommerceService.Data.Repository;
using ECommerceService.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ECommerce.Service.Test
{
    public class CouponControllerTest
    {
        private readonly CouponController _couponController;

        public CouponControllerTest()
        {
            var orderMetaDataRepository = new  Mock<IOrderMetaDataRepository>();
            var couponRepository = new CouponRepository(orderMetaDataRepository.Object);
            var logger = Mock.Of<ILogger<CouponController>>();
            _couponController = new CouponController( couponRepository, logger);

            var orderMetaData = new OrderMetaData()
            {
                Id = "OrderMetaData",
                TotalDiscountAmount= 500,
                TotalPurchaseAmount= 20000,
                NumberOfItemsOrdered= 30,
                NumberOfOrders= 10
            };
            orderMetaDataRepository.Setup(x => x.GetOrderMetaData()).Returns(orderMetaData);
        }


        [Fact]
        public void AddCoupon_ValidInput_ReturnsOkResponse()
        {
            //Arrange
            var coupon = new Coupon()
            {
                Code= "TestCode",
                DisplayName= "TestCouponName",
                DiscountPercentage = 5,
                OrderFrequency = 1
            };
            //Act
            var result_Ok = _couponController.Post( coupon );
    
            //Assert
            Assert.IsType<OkObjectResult>(result_Ok);
        }

        [Fact]
        public void AddCoupon_InValidInput_ReturnsBadRequestResponse()
        {
            //Arrange
            var couponwithEmtpyCode = new Coupon()
            {
                Code = "",
                DisplayName = "TestCouponName",
                DiscountPercentage = 5,
                OrderFrequency = 1
            };
            //Act
            var result_Ok = _couponController.Post(couponwithEmtpyCode);

            //Assert
            Assert.IsType<BadRequestResult>(result_Ok);
        }

        [Fact]
        public void AddCoupon_DuplicateInput_ReturnsConflictResponse()
        {
            //Arrange
            var coupon = new Coupon()
            {
                Code = "TestCouponCode",
                DisplayName = "TestCoupon 1st time",
                DiscountPercentage = 5,
                OrderFrequency = 1
            };
            _couponController.Post(coupon);

            var couponWithDuplicateCode = new Coupon()
            {
                Code = "TestCouponCode",
                DisplayName = "TestCoupon 2nd time",
                DiscountPercentage = 10,
                OrderFrequency = 2
            };
            //Act
            var result_Conflict = _couponController.Post(couponWithDuplicateCode);

            //Assert
            Assert.IsType<ConflictObjectResult>(result_Conflict);
        }
    }
}
