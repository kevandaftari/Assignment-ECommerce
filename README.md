# Assignment-ECommerce
ECommerce BackEnd Assignment for UniBlox.

The project contains below APIs:

 **Admin Access APIs**
 
1) Coupon/GetListOfCoupons : It returns list of all coupons available in the system.
2) Coupon/Post : It Adds new coupon in the system.
            Input : Body should be of following type.
            
                     {
                        "code": "TestCouponToAdd",
                        "displayName": "TestCoupon20%",
                        "discountPercentage": 20,
                        "orderFrequency": 10        // orderFrequency (n) : At next n th order , coupon code will be applicable.
                      }
3) MetaData/GetOrderMetaData : It returns all the metaData related to orders. Total Purchase Amount , total discount amount ,number of orders and number of items ordered of all the users combined.                     

**User Access APIs**

1) Cart/AddItem : It adds a single item to the cart. 
                 Input : Body should be of following type.
                 
                       {
                          "userId" : 1,     // Id of the user for which item will be added in the cart.
                           "itemId" : 1,    // Id of the Item.
                           "count"  : 2     // how many quntity of the item you wish to add.
                       }
                       
 2) Order/PlaceOrder : It empties the cart and places the order.
                 Input : 
                 
                        {
                             "userId" : 1 ,   // Id of the user for placing the order.
                             "couponCode" : "Default", //  coupon code name. Remove this field if coupon is not applicable.
                         }
                     
