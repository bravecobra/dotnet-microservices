using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using deliveries.Models;

namespace deliveries.Repositories
{
    public interface IDeliveriesRepository
    {
        Task<IEnumerable<Delivery>> GetOrderDeliveries(Guid orderId);
        Task<Delivery> GetDelivery(Guid id);
    }
}
