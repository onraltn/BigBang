using BigBang.Order.Domain.Repositories;
using BigBang.Order.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Order.Persistence.Repositories
{
    public class OrderRepository : Repository<Order.Domain.Aggregates.OrderAggregate.Order>, IOrderRepository
    {
        private readonly OrderDbContext _orderDbContext;

        public OrderRepository(OrderDbContext orderDbContext) : base(orderDbContext)
        {
            this._orderDbContext = orderDbContext;
        }

        public async Task<Domain.Aggregates.OrderAggregate.Order> GetOrder(string trackingNumber)
        {
            return await _orderDbContext.Orders.Include(o => o.Items)
                .Where(x => x.TrackingNumber == trackingNumber).SingleOrDefaultAsync();
        }
    }
}
