using BigBang.Order.Domain.Repositories;
using Convey.CQRS.Queries;

namespace BigBang.Order.Application.ApplicationQueries.GetOrderByTrackingNumber
{
    public sealed class GetOrderByTrackingNumberQueryHandler : IQueryHandler<GetOrderByTrackingNumberQuery, GetOrderByTrackingNumberQueryResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByTrackingNumberQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GetOrderByTrackingNumberQueryResponse> HandleAsync(GetOrderByTrackingNumberQuery query, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetOrder(query.TrackingNumber);
            if (order == null) throw new ArgumentNullException(nameof(query));

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
