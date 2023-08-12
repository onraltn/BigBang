using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBang.Order.Domain.Aggregates.OrderAggregate
{
    [Table("OrderItem", Schema = "SALES")]
    public class OrderItem : BaseEntity<long>
    {
        public string ItemName { get; private set; }
        public uint Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        internal OrderItem(string itemName, uint quantity, decimal unitPrice)
        {
            if (string.IsNullOrEmpty(itemName)) throw new ArgumentException($"'{nameof(itemName)}' cannot be null or empty.", nameof(itemName));
            if (quantity == 0) throw new ArgumentException("Quantity must be at least one.", nameof(quantity));
            if (unitPrice <= 0) throw new ArgumentException("Unit price must be above zero.", nameof(unitPrice));

            this.ItemName = itemName;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
        }
        internal void AddQuantity(uint quantity)
        {
            this.Quantity += quantity;
        }
        internal void WithdrawQuantity(uint quantity)
        {
            if (this.Quantity - quantity <= 0) throw new InvalidOperationException("Can't remove all units. Remove the entire item instead.");
            this.Quantity -= quantity;
        }
    }
}
