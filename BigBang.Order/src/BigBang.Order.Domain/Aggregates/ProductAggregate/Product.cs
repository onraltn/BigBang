using System;
using BigBang.Order.Domain.Aggregates.CompanyAggregate;

namespace BigBang.Order.Domain.Aggregates.ProductAggregate
{
    public class Product : BaseEntity<long>
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<OrderAggregate.Order> Orders { get; set; }
    }
}
