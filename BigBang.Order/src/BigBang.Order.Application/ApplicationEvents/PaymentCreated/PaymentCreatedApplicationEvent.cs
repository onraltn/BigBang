using Convey.CQRS.Events;

namespace BigBang.Order.Application.ApplicationEvents.PaymentCreated
{
    public sealed record PaymentCreatedApplicationEvent : IEvent
    {
        public decimal Amount { get; }
        public string TrackingNumber { get; }

        public PaymentCreatedApplicationEvent(decimal amount, string trackingNumber)
        {
            Amount = amount;
            TrackingNumber = trackingNumber;
        }
    }
}
