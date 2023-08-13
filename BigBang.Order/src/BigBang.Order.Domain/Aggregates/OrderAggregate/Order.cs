using BigBang.Order.Domain.Aggregates.OrderAggregate.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBang.Order.Domain.Aggregates.OrderAggregate
{
    [Table("Order", Schema = "SALES")]
    public class Order : Entity, IAggregateRoot
    {
        [Key]
        public long Id { get; set; }
        public string TrackingNumber { get; private set; }
        public Guid OrderNumber { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal PaidAmount { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? EstimatedShippingDate { get; private set; }
        public Address Address { get; private set; }
        [ForeignKey("OrderId")]
        public virtual ICollection<OrderItem> Items { get; private set; }

        public decimal OrderTotal => Items.Sum(x => Convert.ToDecimal(x.Quantity) * x.UnitPrice);

        public Order()
        {
            
        }
        public Order(decimal paidAmount, DateTime? shippingDate, Address address, string trackingNumber)
        {
            this.Status = OrderStatus.PaymentPending;
            this.CreatedDate = DateTime.Now;
            this.TrackingNumber = trackingNumber;
            this.PaidAmount = paidAmount;
            this.EstimatedShippingDate = shippingDate;
            this.Address = address;
            this.OrderNumber = Guid.NewGuid();
            this.Items = new List<OrderItem>();
        }

        public void AddPayment(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount must be positive.");
            if (amount > OrderTotal - PaidAmount)
                throw new InvalidOperationException("Payment can't exceed order total");

            PaidAmount += amount;
            if (PaidAmount >= OrderTotal)
                Status = OrderStatus.ReadyForShipping;
        }

        public void AddItem(string itemName, uint quantity, decimal unitPrice)
        {
            if (Status != OrderStatus.PaymentPending) throw new InvalidOperationException("Can't modify order once payment has been done.");
            Items.Add(new OrderItem(itemName, quantity, unitPrice));
        }

        public void RemoveItem(string itemName)
        {
            if (Status != OrderStatus.PaymentPending) throw new InvalidOperationException("Can't modify order once payment has been done.");
            Items.ToList().RemoveAll(x => x.ItemName == itemName);
        }

        public void ShipOrder()
        {
            if (Items.Sum(x => x.Quantity) <= 0) throw new InvalidOperationException("Can´t ship an order with no items.");
            if (Status == OrderStatus.PaymentPending) throw new InvalidOperationException("Can´t ship order unpaid order.");
            if (Status == OrderStatus.InTransit) throw new InvalidOperationException("Order already shipped to customer.");

            EstimatedShippingDate = DateTime.Now.AddDays(5);
            Status = OrderStatus.InTransit;
        }

        private string GenerateTrackingNumber()
        {
            return $"BNG{DateTime.Now:ddMMyyyyHHmmss}";
        }
    }
}

