using BigBang.Order.Application.ApplicationEvents.PaymentCreated;
using BigBang.Order.Infrastructure.Events;
using Convey.CQRS.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Order.Consumer.Consumers
{
    public sealed class PaymentCreatedConsumer : IConsumer<PaymentCreatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentCreatedConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Consume(ConsumeContext<PaymentCreatedIntegrationEvent> context)
        {
            ILogger<PaymentCreatedConsumer> logger = _serviceProvider.GetRequiredService<ILogger<PaymentCreatedConsumer>>();
            try
            {

                var message = context.Message;
                ArgumentNullException.ThrowIfNull(message);

                logger.LogInformation(message: JsonConvert.SerializeObject(message));

                var eventDispatcher = _serviceProvider.GetRequiredService<IEventDispatcher>();
                await eventDispatcher.PublishAsync(new PaymentCreatedApplicationEvent()
                {

                });

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
