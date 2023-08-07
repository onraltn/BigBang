using BigBang.Order.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Events
{
    public record OrderStatusUpdatedEvent : IEvent
    {
        public string TrackingNumber { get; set; }
        public string OrderStatus { get; set; }
        public Guid CollerationId { get; set; }
    }
}
