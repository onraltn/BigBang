using Convey.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Order.Application.ApplicationQueries.GetOrderByTrackingNumber
{
    public sealed record GetOrderByTrackingNumberQuery : IQuery<GetOrderByTrackingNumberQueryResponse>
    {
        public string TrackingNumber { get; set; }
    }
}
