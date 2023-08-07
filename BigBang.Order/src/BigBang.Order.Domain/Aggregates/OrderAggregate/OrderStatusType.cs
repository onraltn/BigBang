using System;
using System.ComponentModel.DataAnnotations;

namespace BigBang.Order.Domain.Aggregates.OrderAggregate
{
    [MetadataType(typeof(Enumeration))]
    public class OrderStatusType : Enumeration
    {
        public static OrderStatusType Completed = new OrderStatusType(1, "completed", "sipariş tamamlandı");

        public OrderStatusType(int id, string name, string displayName) : base(id, name, displayName)
        {

        }
        public static IEnumerable<OrderStatusType> List() => new OrderStatusType[] { Completed };

        public static OrderStatusType Get(int id)
        {
            var state = List().Single(x => x.Id == id);
            return state ?? throw new ArgumentNullException();

        }

        public static OrderStatusType Get(string name)
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

