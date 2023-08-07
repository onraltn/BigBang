using BigBang.Order.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Events
{
    public record OrderCreatedEvent : IEvent
    {
        public Guid CollerationId { get; set; }
        public string TrackingNumber { get; set; }
        public string CustomerName { get;set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CreateDate { get; set; }
    }
}
