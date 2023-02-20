# Assignment-ECommerce
ECommerce BackEnd Assignment for UniBlox.

The project contains below APIs:
 Admin Access APIs
1) Coupon/GetListOfCoupons : It returns list of all coupons available in the system.
2) Coupon/Post : It Adds new coupon in the system.
            Input : Body should be of following type.
                      ```
                     {
                        "code": "TestCouponToAdd",
                        "displayName": "TestCoupon20%",
                        "discountPercentage": 20,
                        "orderFrequency": 10
                      }
                      ```
