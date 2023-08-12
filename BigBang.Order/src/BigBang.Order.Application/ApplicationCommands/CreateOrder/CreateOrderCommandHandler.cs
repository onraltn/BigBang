using BigBang.Order.Domain.Aggregates.OrderAggregate;
using BigBang.Order.Persistence.Context;
using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Order.Application.ApplicationCommands.CreateOrder
{
    public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly OrderDbContext _dbContext;

        public CreateOrderCommandHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            var order = new Order.Domain.Aggregates.OrderAggregate.Order(command.Amount,null, new Domain.Aggregates.OrderAggregate.ValueObjects.Address()
            {
                City = command.City,
                Country = command.Country,
            });

            await _dbContext.AddAsync(order, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
    }
}
