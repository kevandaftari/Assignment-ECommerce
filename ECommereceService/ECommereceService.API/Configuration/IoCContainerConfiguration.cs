
namespace ECommerceService.API.Configuration
{
    public static class IoCContainerConfiguration
    {
        public static void ConfigureService(IServiceCollection services)
        {
            Data.Configuration.IoCContainerConfiguration.ConfigureService(services);
        }
    }
}
