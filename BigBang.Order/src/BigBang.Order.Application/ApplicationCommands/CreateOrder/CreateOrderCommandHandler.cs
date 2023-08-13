using BigBang.Order.Domain.Repositories;
using Convey.CQRS.Commands;

namespace BigBang.Order.Application.ApplicationCommands.CreateOrder
{
    public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public async Task HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
        {
            var order = new Order.Domain.Aggregates.OrderAggregate.Order(command.Amount,null, new Domain.Aggregates.OrderAggregate.ValueObjects.Address()
            {
                City = command.City,
                Country = command.Country,
            }, command.TrackingNumber);

            foreach (var item in command.Items)
            {
                order.AddItem(item.ItemName, item.Quantity, item.UnitPrice);
            }

            await _orderRepository.AddAsync(order);
        }
    }
}
