using System;
using System.ComponentModel.DataAnnotations;

namespace BigBang.Order.Domain.Aggregates.OrderAggregate
{
    [MetadataType(typeof(Enumeration))]
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Completed = new(1, "completed", "sipariş tamamlandı");
        public static OrderStatus PaymentPending = new(2, "payment_pending", "Ödeme bekleniyor");
        public static OrderStatus ReadyForShipping = new(3, "ready_for_shipping", "Gönderim için bekleniyor");
        public static OrderStatus InTransit = new(3, "out_for_delivery", "Kargo dağıtımda");

        public OrderStatus(int id, string name, string displayName) : base(id, name, displayName)
        {

        }
        public static IEnumerable<OrderStatus> List() => new OrderStatus[] { Completed, PaymentPending, ReadyForShipping };

        public static OrderStatus Get(int id)
        {
            var state = List().Single(x => x.Id == id);
            return state ?? throw new ArgumentNullException();

        }

        public static OrderStatus Get(string name)
        {
            var state = List().SingleOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (state is null)
            {
                state = List().SingleOrDefault(x => string.Equals(x.DisplayName, name, StringComparison.CurrentCultureIgnoreCase));
            }

            if (state == null) throw new ArgumentNullException();

            return state;
        }

    }
}

