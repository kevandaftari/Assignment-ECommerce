
using ECommerceService.Business.IServices;
using ECommerceService.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceService.Business.Configuration
{
    public static class IoCContainerConfiguration
    {
        public static void ConfigureService(IServiceCollection services)
        {
            //services.AddScoped<ICouponService, CouponService>();
        }
    }
}
