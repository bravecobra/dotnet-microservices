using System;
using ordering.Domain;

namespace ordering.Controllers.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public OrderStates State { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
