using System;
using BigBang.Order.Domain.Aggregates.ProductAggregate;

namespace BigBang.Order.Domain.Aggregates.CompanyAggregate
{
    public class Company : BaseEntity<long>
    {
        public string Name { get; set; }
        public bool IsConfirmed { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<OrderAggregate.Order> Orders { get; set; }
    }
}

