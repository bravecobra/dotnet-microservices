using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ordering.Models;

namespace ordering.Repositories
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> GetClientOrders(Guid clientId);
        Task<Order> GetOrder(Guid orderId);
    }
}
