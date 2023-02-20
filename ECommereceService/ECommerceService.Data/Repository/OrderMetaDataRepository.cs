using ECommerceService.Data.IRepository;
using ECommerceService.Entity.Models;
using static ECommerceService.Entity.EFCoreInMemoryDb;

namespace ECommerceService.Data.Repository
{
    public class OrderMetaDataRepository : IOrderMetaDataRepository
    {
        public OrderMetaData? GetOrderMetaData()
        {
            using (var context = new ApiContext())
            {
                var item = context.OrderMetaData.FirstOrDefault(x => x.Id == "OrderMetaData");
                return item;
            }
        }

        public OrderMetaData? UpdateOrderMetaData(OrderMetaData orderMetaData)
        {
            using (var context = new ApiContext())
            {
                context.OrderMetaData.Update(orderMetaData);
                context.SaveChanges();
                return orderMetaData;
            }
        }


    }
}
