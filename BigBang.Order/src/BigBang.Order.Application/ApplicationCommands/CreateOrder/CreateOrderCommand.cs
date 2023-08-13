using Convey.CQRS.Commands;
using System.Text.Json.Serialization;

namespace BigBang.Order.Application.ApplicationCommands.CreateOrder
{
    public sealed record CreateOrderCommand : ICommand
    {
        public decimal Amount { get; }
        public string City { get; }
        public string Country { get; }
        public string TrackingNumber { get; private set; }
        public ICollection<OrderItemCommandModel> Items { get; }

        public CreateOrderCommand(decimal amount, string city, string country, ICollection<OrderItemCommandModel> items, string trackingNumber)
        {
            Amount = amount;
            City = city;
            Country = country;
            Items = items;
            TrackingNumber = trackingNumber;
        }

        public string GenerateTrackingNumber()
        {
            this.TrackingNumber = $"BNG{DateTime.Now:ddMMyyyyHHmmss}";
            return this.TrackingNumber ;
        }
    }

    public class OrderItemCommandModel
    {
        public string ItemName { get; set; }
        public uint Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
