using System;
using System.ComponentModel.DataAnnotations;

namespace BigBang.Order.Domain.Aggregates
{
    public abstract class BaseEntity<T> where T : notnull
    {
        [Key]
        public T Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Status { get; set; }
    }
}

