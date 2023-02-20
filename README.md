# Assignment-ECommerce
ECommerce BackEnd Assignment for UniBlox.

Default Data :
   I have intialized default data required to run the APIs.
   It will automatically run when program starts.
   Location : ECommereceService.Entity Project => EFCoreInMemoryDb.cs => void InitializeDefaulData() method.
   If you want to change default data you can do it in this function.

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
                       
      Output : If ItemId is invalid( item is not present in inventory system ) or count is more than itemRemaining Count in inventory.
                        
                        Item not found or request count of item is more than inventory
                       
 2) Order/PlaceOrder : It empties the cart and places the order.
 
       Input : 
                 
                        {
                             "userId" : 1 ,   // Id of the user for placing the order.
                             "couponCode" : "DefaultCouponCode10", //  coupon code name. Remove this field if coupon is not applicable.
                        }
       
       Output :  If Order is successful.
    
                        {
                             "orderId": "10512a66-f785-4b3d-ba9c-fcf656ab9fbb",
                             "userId": 1,
                             "isDiscountApplied": true,
                             "couponCodeName": "DefaultCouponCode10",
                             "couponDisplayName": "DefaultCoupon10%",
                             "discountPercentage": 10.0,
                             "discountAmount": 6.0,
                             "orderItemDetails": {
                                 "numberOfItemsInOrder": 2,
                                 "orderItems": [
                                     {
                                         "itemId": 1,
                                         "itemName": "Default Product 1",
                                         "itemDescription": "Product Description 1",
                                         "itemPrice": 30.0,
                                         "itemCount": 2,
                                         "orderIdFK": "10512a66-f785-4b3d-ba9c-fcf656ab9fbb"
                                     }
                                 ],
                                 "orderIdFK": "10512a66-f785-4b3d-ba9c-fcf656ab9fbb"
                             },
                             "grossAmount": 60.0,
                             "finalPurchaseAmount": 54.0
                         }
  
  Output :  If Coupon Code is invalid.
  
                 Invalid Coupon Code or Coupon Code not Applicable for current order.
                 
  Output : If  Cart is empty.
  
                 Cart is Empty.Please Add minimum one Item.
