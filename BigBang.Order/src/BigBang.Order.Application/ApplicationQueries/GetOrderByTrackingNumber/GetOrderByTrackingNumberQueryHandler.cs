using BigBang.Order.Persistence.Context;
using Convey.CQRS.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Order.Application.ApplicationQueries.GetOrderByTrackingNumber
{
    public sealed class GetOrderByTrackingNumberQueryHandler : IQueryHandler<GetOrderByTrackingNumberQuery, GetOrderByTrackingNumberQueryResponse>
    {
        private readonly OrderDbContext _dbContext;

        public GetOrderByTrackingNumberQueryHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetOrderByTrackingNumberQueryResponse> HandleAsync(GetOrderByTrackingNumberQuery query, CancellationToken cancellationToken = default)
        {
            var order = await _dbContext.Orders.Where(x => x.TrackingNumber == query.TrackingNumber).FirstOrDefaultAsync() ?? throw new ArgumentNullException(nameof(query));

            return new GetOrderByTrackingNumberQueryResponse()
            {
                CreateDate = order.CreatedDate,
                OrderNumber = order.OrderNumber,
                PaidAmount = order.PaidAmount,
                ShippingDate = order.EstimatedShippingDate,
                Status = order.Status.DisplayName,
                OrderItems = order.Items.Select(x => new OrderItemQueryReponse()
                {
                    ItemName = x.ItemName,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }).ToList()
            };
        }
    }
}
