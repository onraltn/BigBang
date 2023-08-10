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
        public Guid CollerationId { get; set; }
    }
}
