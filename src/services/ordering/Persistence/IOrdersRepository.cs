using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ordering.Domain;

namespace ordering.Persistence
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> GetClientOrders(Guid clientId);
        Task<Order> GetOrder(Guid orderId);
    }
}
