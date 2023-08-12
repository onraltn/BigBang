using Convey.CQRS.Commands;

namespace BigBang.Order.Application.ApplicationCommands.CreateOrder
{
    public sealed record CreateOrderCommand : ICommand
    {
        public decimal Amount { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<OrderItemCommandModel> Items { get; set; }
    }

    public class OrderItemCommandModel
    {
        public string ItemName { get; set; }
        public uint Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
