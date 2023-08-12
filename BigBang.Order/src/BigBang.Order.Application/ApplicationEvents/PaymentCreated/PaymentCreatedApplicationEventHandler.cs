using BigBang.Order.Persistence.Context;
using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Order.Application.ApplicationEvents.PaymentCreated
{
    public class PaymentCreatedApplicationEventHandler : IEventHandler<PaymentCreatedApplicationEvent>
    {
        private readonly OrderDbContext _dbContext;

        public PaymentCreatedApplicationEventHandler(OrderDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task HandleAsync(PaymentCreatedApplicationEvent @event, CancellationToken cancellationToken = default)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var order = _dbContext.Orders.Where(x => x.OrderNumber == @event.OrderNumber).FirstOrDefault() ?? throw new ArgumentNullException("Order not found");
            order.AddPayment(@event.Amount);

            _dbContext.Orders.Update(order);
            await transaction.CommitAsync(cancellationToken);
        }
    }
}
