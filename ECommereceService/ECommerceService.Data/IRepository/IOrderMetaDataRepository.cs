using ECommerceService.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Data.IRepository
{
    public interface IOrderMetaDataRepository
    {
        public OrderMetaData? GetOrderMetaData();
        public OrderMetaData? UpdateOrderMetaData(OrderMetaData orderMetaData);

    }
}
