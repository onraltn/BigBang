using BigBang.Order.Domain.Repositories;
using Convey.CQRS.Events;

namespace BigBang.Order.Application.ApplicationEvents.PaymentCreated
{
    public class PaymentCreatedApplicationEventHandler : IEventHandler<PaymentCreatedApplicationEvent>
    {
        private readonly IOrderRepository _orderRepository;

        public PaymentCreatedApplicationEventHandler(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }
        public async Task HandleAsync(PaymentCreatedApplicationEvent @event, CancellationToken cancellationToken = default)
        {

            var order = await _orderRepository.GetOrder(@event.TrackingNumber) ?? throw new ArgumentNullException("Order not found");
            order.AddPayment(@event.Amount);

            await _orderRepository.UpdateAsync(order);
        }
    }
}
