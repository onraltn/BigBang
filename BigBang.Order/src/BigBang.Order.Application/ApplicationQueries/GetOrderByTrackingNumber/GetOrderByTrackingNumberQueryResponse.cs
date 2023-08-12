namespace BigBang.Order.Application.ApplicationQueries.GetOrderByTrackingNumber
{
    public class GetOrderByTrackingNumberQueryResponse
    {
        public GetOrderByTrackingNumberQueryResponse()
        {
            OrderItems = new List<OrderItemQueryReponse>();
        }
        public Guid OrderNumber { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime? ShippingDate { get; set; }
        public ICollection<OrderItemQueryReponse> OrderItems { get; set; }
    }

    public class OrderItemQueryReponse
    {
        public string ItemName { get; set; }
        public uint Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
