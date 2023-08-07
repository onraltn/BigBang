using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Order.Infrastructure.Events
{
    public interface IEvent
    {
        Guid CollerationId { get; }
    }
}
