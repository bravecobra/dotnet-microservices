using System;

namespace ordering.Domain
{
    public class Order
    {
        public Guid Id { get; set; }

        public OrderStates State { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }

        public Guid ClientId { get; set; }
    }
}