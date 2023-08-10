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
        public Task HandleAsync(PaymentCreatedApplicationEvent @event, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
