using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Order.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order.Domain.Aggregates.OrderAggregate.Order>
    {
        Task<Order.Domain.Aggregates.OrderAggregate.Order> GetOrder(string trackingNumber);
    }
}
