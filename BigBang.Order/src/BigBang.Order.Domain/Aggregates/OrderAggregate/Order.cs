using System;
using BigBang.Order.Domain.Aggregates.CompanyAggregate;
using BigBang.Order.Domain.Aggregates.ProductAggregate;

namespace BigBang.Order.Domain.Aggregates.OrderAggregate
{
    public class Order : BaseEntity<Guid>
    {
        public string TrackingNumber { get; set; }
        public OrderStatusType Status { get; set; }

        //Address
        public string Street { get; private set; }
        public string City { get; private set; }
        public int CompanyId { get; set; }
        public int ProductId { get; set; }
        public string CustomerName { get; set; }
        public Product Product { get; set; }
        public Company Company { get; set; }
    }
}

