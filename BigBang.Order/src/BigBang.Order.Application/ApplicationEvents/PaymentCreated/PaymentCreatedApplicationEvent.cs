using Convey.CQRS.Events;

namespace BigBang.Order.Application.ApplicationEvents.PaymentCreated
{
    public sealed record PaymentCreatedApplicationEvent : IEvent
    {
        public decimal Amount { get; }
        public Guid OrderNumber { get; }

        public PaymentCreatedApplicationEvent(decimal amount, Guid orderNumber)
        {
            Amount = amount;
            OrderNumber = orderNumber;
        }
    }
}
