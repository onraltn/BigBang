using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Order.Infrastructure.Events
{
    public sealed record PaymentCreatedIntegrationEvent
    {
        public decimal Amount { get; }
        public Guid OrderNumber { get; }

        public PaymentCreatedIntegrationEvent(decimal amount, Guid orderNumber)
        {
            Amount = amount;
            OrderNumber = orderNumber;
        }
    }
}
