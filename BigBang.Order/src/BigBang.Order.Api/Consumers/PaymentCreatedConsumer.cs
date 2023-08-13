using BigBang.Order.Application.ApplicationEvents.PaymentCreated;
using BigBang.Order.Infrastructure.Events;
using Convey.CQRS.Events;
using MassTransit;
using Newtonsoft.Json;

namespace BigBang.Order.Api.Consumers
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

                logger.LogInformation(message: JsonConvert.SerializeObject(message), context.CorrelationId);

                var eventDispatcher = _serviceProvider.GetRequiredService<IEventDispatcher>();
                await eventDispatcher.PublishAsync(new PaymentCreatedApplicationEvent(message.Amount, message.TrackingNumber));

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
