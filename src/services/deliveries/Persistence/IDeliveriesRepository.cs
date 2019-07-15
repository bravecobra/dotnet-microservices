using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using deliveries.Domain;

namespace deliveries.Persistence
{
    public interface IDeliveriesRepository
    {
        Task<IEnumerable<Delivery>> GetOrderDeliveries(Guid orderId);
        Task<Delivery> GetDelivery(Guid id);
    }
}
