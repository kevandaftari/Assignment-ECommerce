using ECommerceService.Entity.Models;
using ECommerceService.Entity.Models.OrderDetails;
using Microsoft.EntityFrameworkCore;

namespace ECommerceService.Entity
{
    public class EFCoreInMemoryDb
    {
        public class ApiContext : DbContext
        {
            protected override void OnConfiguring
           (DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: "OrderDb");
            }
            public DbSet<OrderMetaData> OrderMetaData { get; set; }
            public DbSet<Item> Items { get; set; }
            public DbSet<Cart> Carts { get; set; }
            public DbSet<CartItemDetails> CartItemDetails { get; set; }
            public DbSet<Coupon> Coupons { get; set; }
            public DbSet<OrderDetails> ListOfOrderDetails { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

                modelBuilder.Entity<CartItemDetails>()
                    .HasKey(cd => new { cd.CartUserId, cd.ItemId });

                modelBuilder.Entity<CartItemDetails>()
                     .HasOne(cDetails => cDetails.Cart)
                     .WithMany(c => c.Items)
                     .HasForeignKey(cDetails => cDetails.CartUserId);


                modelBuilder.Entity<OrderDetails>()
                    .HasOne(orderDetails => orderDetails.OrderItemDetails)
                    .WithOne(orderItemsDetails => orderItemsDetails.orderDetails)
                    .HasForeignKey<OrderItemsDetails>(orderItemsDetails => orderItemsDetails.OrderIdFK);

                modelBuilder.Entity<OrderItemsDetails>()
                    .HasKey(od => new { od.OrderIdFK });

                modelBuilder.Entity<OrderItem>()
                     .HasOne(oItem => oItem.orderItemsDetails)
                     .WithMany(oItemDetails => oItemDetails.OrderItems)
                     .HasForeignKey(oItem => oItem.OrderIdFK);

                modelBuilder.Entity<OrderItem>()
                    .HasKey(orderItem => new { orderItem.OrderIdFK , orderItem.ItemId });

            }
        }

        public static void InitializeDefaultData()
        {
            using (var context = new ApiContext())
            {
                InitializeDefaultCoupons(context);
                InitializeDefaultItems(context);
                InitializeOrderMetaData(context);
                context.SaveChanges();
            }
        }

        private static void InitializeOrderMetaData(ApiContext context)
        {
            var orderMetaData = new OrderMetaData()
            {
                Id = "OrderMetaData",
                TotalDiscountAmount = 0,
                TotalPurchaseAmount = 0,
                NumberOfItemsOrdered = 0,
                NumberOfOrders = 0,
            };
            context.OrderMetaData.Add(orderMetaData);
        }

        private static void InitializeDefaultItems(ApiContext context)
        {
            var items = new List<Item>() {
                new Item()
                {
                    Id = 1,
                    Name= "Default Product 1",
                    ItemDescription = "Product Description 1",
                    RemainingCount = 100,
                    NumberOfPurchases = 0,
                    Price = 30
                },
                new Item()
                {
                    Id = 2,
                    Name= "Default Product 2",
                    ItemDescription = "Product Description 2",
                    RemainingCount = 30,
                    NumberOfPurchases = 3,
                    Price = 40
                },
                new Item()
                {
                    Id = 3,
                    Name= "Default Product 3",
                    ItemDescription = "Product Description 3",
                    RemainingCount = 40,
                    NumberOfPurchases = 2,
                    Price = 1000
                }
            };
            context.Items.AddRange(items);
        }

        private static void InitializeDefaultCoupons(ApiContext context)
        {
            var coupons = new List<Coupon>
                {
                new Coupon
                {
                    Code= "DefaultCouponCode10",
                    DisplayName = "DefaultCoupon10%",
                    DiscountPercentage = 10,
                    OrderFrequency= 5,
                    OrderStart = 1
                }
                };
            context.Coupons.AddRange(coupons);
        }
    }
}

