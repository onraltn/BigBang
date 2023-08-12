using System.ComponentModel.DataAnnotations.Schema;

namespace BigBang.Order.Domain.Aggregates.OrderAggregate.ValueObjects
{
    [Table("OrderAddress", Schema = "SALES")]
    public class Address : ValueObject
    {
        public string City { get; set; }
        public string Country { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return City;
            yield return Country;
        }
    }
}
