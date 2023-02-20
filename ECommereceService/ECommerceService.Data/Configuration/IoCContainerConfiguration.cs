using ECommerceService.Data.IRepository;
using ECommerceService.Data.Repository;
using ECommerceService.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceService.Data.Configuration
{
    public static class IoCContainerConfiguration
    {
        public static void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<ICartRepository,CartRepository>();
            services.AddScoped<ICartItemDetailsRepository,CartItemDetailsRepository>();
            services.AddScoped<IOrderMetaDataRepository,OrderMetaDataRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();

            EFCoreInMemoryDb.InitializeDefaultData();
        }
    }
}
